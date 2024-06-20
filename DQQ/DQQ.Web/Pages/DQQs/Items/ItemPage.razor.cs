using DQQ.Components.Stages.Actors.Characters;
using Microsoft.AspNetCore.Components;

namespace DQQ.Web.Pages.DQQs.Items
{
  public class ItemPagePage : DQQPageBase
  {
     protected override void OnParametersSet()
    {
      base.OnParametersSet();
      StateHasChanged();
    }
    public async Task ShowPickList()
    {
      await dialogService.ShowComponent<ItemPickList>(
        new Dictionary<string, object?>
        {
          ["ActorId"] = ActorId,
        }, "");
    }
    public async Task ShowInventory()
    {
      await dialogService.ShowComponent<ActorInventory>(
        new Dictionary<string, object?>
        {
          ["ActorId"] = ActorId,
          ["ParentRefreshEvent"] = ParentRefreshEvent,
          ["SelectedCharacter"] = SelectedCharacter,
          ["ParentGuid"] = ParentGuid,

        }, "");
    }
  }
}