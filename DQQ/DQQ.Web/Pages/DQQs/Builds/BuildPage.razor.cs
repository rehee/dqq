using DQQ.Components.Stages.Actors;
using DQQ.Components.Stages.Actors.Characters;
using DQQ.Web.Pages.DQQs.Items;
using Microsoft.AspNetCore.Components;

namespace DQQ.Web.Pages.DQQs.Builds
{
  public class BuildPagePage : DQQPageBase
  {
    [Parameter]
    public Character? SelectedCharacter { get; set; }

    protected override async Task OnParametersSetAsync()
    {
      await base.OnParametersSetAsync();
      StateHasChanged();
    }

    public async Task ShowInventory()
    {
      await dialogService.ShowComponent<ActorInventory>(
        new Dictionary<string, object?>
        {
          ["ActorId"] = SelectedCharacter?.DisplayId,
          ["ParentRefreshEvent"] = ParentRefreshEvent,
          ["SelectedCharacter"] = SelectedCharacter,
          ["ParentGuid"] = ParentGuid,

        }, "");
    }
  }
}