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

      // 通过 options 获得用户组合的过滤条件
      var filters = options.ToFilter();

      // 使用内置扩展方法 ToFilter 获得过滤条件
      var items = Items.Where(filters.GetFilterFunc<ItemEntity>());
      if (Rarity != null)
      {
        items = items.Where(b => b.Rarity.Equals(Rarity));
      }
      if (ItemType != null)
      {
        items = items.Where(b => b.ItemType.Equals(ItemType));
      }
      // 排序标记
      var isSorted = false;

      //处理高级排序
      if (options.AdvancedSortList.Count != 0)
      {
        items = items.Sort(options.AdvancedSortList);
        isSorted = true;
      }

      // 此段代码可不写，组件内部自行处理
      if (options.SortName == nameof(ItemEntity.Name))
      {
        items = items.Sort(options.SortList);
        isSorted = true;
      }
      else if (!string.IsNullOrEmpty(options.SortName))
      {
        // 外部未进行排序，内部自动进行排序处理
        items = items.Sort(options.SortName, options.SortOrder);
        isSorted = true;
      }

      // 设置记录总数
      var enumerable = items.ToList();
      var total = enumerable.Count;

      // 内存分页
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