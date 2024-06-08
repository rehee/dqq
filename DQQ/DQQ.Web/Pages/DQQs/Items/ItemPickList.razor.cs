using BootstrapBlazor.Components;
using DQQ.Entities;
using DQQ.Enums;
using DQQ.Services.ItemServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace DQQ.Web.Pages.DQQs.Items
{
  public class ItemPickListPage : DQQPageBase
  {
    [Inject]
    [NotNull]
    public IItemService? itemService { get; set; }

    public IEnumerable<ItemEntity>? Items { get; set; }

    public EnumRarity? Rarity { get; set; }
    public EnumItemType? ItemType { get; set; }
    public List<ItemEntity> SelectedItems { get; set; } = new List<ItemEntity>();
    public static IEnumerable<int> PageItemsSource => [10, 20, 50];

    [Inject]
    [NotNull]
    public IComponentHtmlRenderer? HtmlRenderer { get; set; }
    public async Task SelectAll()
    {
      await Task.CompletedTask;
      if (ShowSelectAll)
      {
        SelectedItems.Clear();
        SelectedItems.AddRange(filtered ?? []);
      }
      else
      {
        SelectedItems.Clear();

      }


      StateHasChanged();
    }
    public async Task PickSelected()
    {
      await itemService.PickItem(ActorId, SelectedItems.Select(b => b.Id).ToArray());
      await Refresh();
      SelectedItems.Clear();
      StateHasChanged();
    }
    public bool ShowSelectAll => filtered == null ? true : filtered.Length > SelectedItems.Count;
    private ItemEntity[]? filtered { get; set; }
    

    public async Task<QueryData<ItemEntity>> OnQueryAsync(QueryPageOptions options)
    {
      if (Items == null)
      {
        return new QueryData<ItemEntity>()
        {
          Items = null,
          TotalCount = 0,
          IsSorted = true,
          IsFiltered = true
        };
      }

      // ͨ�� options ����û���ϵĹ�������
      var filters = options.ToFilter();

      // ʹ��������չ���� ToFilter ��ù�������
      var items = Items.Where(filters.GetFilterFunc<ItemEntity>());
      if (Rarity != null)
      {
        items = items.Where(b => b.Rarity.Equals(Rarity));
      }
      if (ItemType != null)
      {
        items = items.Where(b => b.ItemType.Equals(ItemType));
      }
      // ������
      var isSorted = false;

      //����߼�����
      if (options.AdvancedSortList.Count != 0)
      {
        items = items.Sort(options.AdvancedSortList);
        isSorted = true;
      }

      // �˶δ���ɲ�д������ڲ����д���
      if (options.SortName == nameof(ItemEntity.Name))
      {
        items = items.Sort(options.SortList);
        isSorted = true;
      }
      else if (!string.IsNullOrEmpty(options.SortName))
      {
        // �ⲿδ���������ڲ��Զ�����������
        items = items.Sort(options.SortName, options.SortOrder);
        isSorted = true;
      }

      // ���ü�¼����
      var enumerable = items.ToList();
      var total = enumerable.Count;

      // �ڴ��ҳ
      filtered = enumerable.Skip((options.PageIndex - 1) * options.PageItems).Take(options.PageItems).ToArray();
      foreach (var i in filtered)
      {
        await i.SetComponent(HtmlRenderer);
      }
      return new QueryData<ItemEntity>()
      {
        Items = filtered,
        TotalCount = total,
        IsSorted = isSorted,
        IsFiltered = filters.HasFilters()
      };
    }
    public HashSet<Guid>? ItemPicked { get; set; }
    [Parameter]
    public Guid? ActorId { get; set; }
    public async Task ItemPickSelected(CheckboxState state, Guid id)
    {
      try
      {
        if (state == CheckboxState.Checked)
        {
          ItemPicked!.Add(id);
        }
        else
        {
          ItemPicked!.Remove(id);
        }
      }
      catch
      {

      }

    }

    public async Task Refresh()
    {
      if (ActorId != null)
      {
        Items = await itemService.PickableItems(ActorId.Value);

      }
      else
      {
        Items = null;
      }
      ItemPicked = new HashSet<Guid>();
      StateHasChanged();
    }
    
    protected override async Task OnInitializedAsync()
    {
      await base.OnInitializedAsync();

      await Refresh();
      
    }

    public async override Task<bool> SaveFunction()
    {
      return (await itemService.PickItem(ActorId, ItemPicked.ToArray())).Success;
    }
  }
}