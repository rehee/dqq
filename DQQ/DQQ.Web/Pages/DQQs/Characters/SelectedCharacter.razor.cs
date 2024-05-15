using DQQ.Components.Stages.Actors.Characters;
using DQQ.Enums;
using DQQ.Services.ActorServices;
using DQQ.Web.Pages.DQQs.Skills;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace DQQ.Web.Pages.DQQs.Characters
{
  public class SelectedCharacterPage : DQQPageBase
  {
    [Inject]
    [NotNull]
    public ICharacterService? characterService { get; set; }

    public Character? character { get; set; }

    private void parentRefresh(object sender, EventArgs e)
    {
      Refresh();
    }

    protected override async Task OnInitializedAsync()
    {
      await base.OnInitializedAsync();
      await Refresh();
      if (ParentRefreshEvent != null)
      {
        ParentRefreshEvent.Event += parentRefresh;
      }
    }
    public override async ValueTask DisposeAsync()
    {
      await base.DisposeAsync();
      if (ParentRefreshEvent != null)
      {
        ParentRefreshEvent.Event -= parentRefresh;
      }
    }
    public async Task SelectSkill(int slot, EnumSkill? skillNumber)
    {
      await Task.CompletedTask;
      await dialogService.ShowComponent<SkillSelect>(
       new Dictionary<string, object?>
       {
         ["Slot"] = slot,
         ["ActorId"] = character?.DisplayId,
         ["SelectSkillNumber"] = skillNumber,
       }, "", true, async save => await Refresh());
    }

    public async Task Refresh()
    {
      var selectedCharId = characterService.GetSelectedCharacter();
      if (selectedCharId == null)
      {
        return;
      }
      var selectedChar = await characterService.GetCharacter(selectedCharId.Value);
      if (selectedChar == null)
      {
        characterService.SelectedCharacter(null);
        return;
      }
      character = selectedChar;
      StateHasChanged();
    }
  }
}