
using DQQ.Entities;
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
      Items = await itemService.ActorInventory(characterService.GetSelectedCharacter());
    }
  }
}