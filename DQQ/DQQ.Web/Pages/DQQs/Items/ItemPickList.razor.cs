using BootstrapBlazor.Components;
using DQQ.Entities;
using DQQ.Services.ItemServices;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace DQQ.Web.Pages.DQQs.Items
{
  public class ItemPickListPage : DQQPageBase
  {
    [Inject]
    [NotNull]
    public IItemService? itemService { get; set; }

    public IEnumerable<ItemEntity>? Items { get; set; }

    public HashSet<Guid>? ItemPicked { get; set; }

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

    protected override async Task OnInitializedAsync()
    {
      await base.OnInitializedAsync();

      var id = characterService.GetSelectedCharacter();
      if (id != null)
      {
        Items = await itemService.PickableItems(id.Value);

      }
      else
      {
        Items = null;
      }
      ItemPicked = new HashSet<Guid>();
      StateHasChanged();

    }

    public async override Task<bool> SaveFunction()
    {
      var id = characterService.GetSelectedCharacter();
      return (await itemService.PickItem(id, ItemPicked.ToArray())).Success;
    }
  }
}