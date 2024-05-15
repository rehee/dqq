
using DQQ.Entities;
using DQQ.Enums;
using DQQ.Services.ItemServices;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace DQQ.Web.Pages.DQQs.Items
{
  public class ActorInventoryPage : DQQPageBase
  {
    [Inject]
    [NotNull]
    public IItemService? itemService { get; set; }
    public IEnumerable<ItemEntity>? Items { get; set; }
    protected override async Task OnInitializedAsync()
    {
      await base.OnInitializedAsync();
      await Refresh();
    }

    public async Task EquipItem(Guid? id, EnumEquipSlot? slot)
    {
      var result = await itemService.EquipItem(characterService.GetSelectedCharacter(), id, slot);
      await Refresh();
      if (ParentRefreshEvent != null)
      {
        ParentRefreshEvent?.InvokeEvent(this, EventArgs.Empty);
      }
    }

    public async Task Refresh()
    {
      Items = await itemService.ActorInventory(characterService.GetSelectedCharacter());
      StateHasChanged();
    }
  }
}