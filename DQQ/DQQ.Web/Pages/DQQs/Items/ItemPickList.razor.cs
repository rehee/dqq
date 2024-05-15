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
      StateHasChanged();

    }
  }
}