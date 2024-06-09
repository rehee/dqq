using DQQ.Components.Stages.Actors.Characters;
using DQQ.Enums;
using DQQ.Services.ActorServices;
using DQQ.Web.Pages.DQQs.Skills;
using DQQ.Web.Pages.DQQs.Strategies;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace DQQ.Web.Pages.DQQs.Characters
{
  public class SelectedCharacterPage : DQQPageBase
  {
    [Inject]
    [NotNull]
    public ICharacterService? characterService { get; set; }

    [Parameter]
    public Guid? CharacterId { get; set; }

    [Parameter]
    public Character? Character { get; set; }

    [Parameter]
    public Func<Task>? CleanSelectedChar { get; set; }

    private void parentRefresh(object sender, EventArgs e)
    {
      Refresh();
    }

    protected override async Task OnInitializedAsync()
    {
      await base.OnInitializedAsync();

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
    public async Task SelectSkill(EnumSkillSlot slot, EnumSkill? skillNumber)
    {
      await dialogService.ShowComponent<SkillSelect>(
       new Dictionary<string, object?>
       {
         ["Slot"] = slot,
         ["ActorId"] = CharacterId,
         ["SelectSkillNumber"] = skillNumber,
       }, "", true, async save => await Refresh2());
    }
    public async Task SelectStrategy(EnumSkillSlot slot)
    {
      var skillDTO = Character?.SkillMap?.ContainsKey((EnumSkillSlot)slot) == true ? Character.SkillMap[(EnumSkillSlot)slot] : null;
      await dialogService.ShowComponent<SkillStrategy>(
       new Dictionary<string, object?>
       {
         ["Slot"] = slot,
         ["ActorId"] = CharacterId,
         ["DTO"] = skillDTO,
       }, "", true, async save => await Refresh2());
    }
    public async Task ChangePriority()
    {
      await dialogService.ShowComponent<TargetPriority>(
       new Dictionary<string, object?>
       {
         ["TargetPriority"] = Character?.TargetPriority,
         ["ActorId"] = Character?.DisplayId
       }, "", true, async save => await Refresh2());
    }

    protected override async Task OnParametersSetAsync()
    {
      await base.OnParametersSetAsync();
      await Refresh();
    }
    public async Task Refresh2()
    {
      await Refresh();
      ParentRefreshEvent?.InvokeEvent(this, null);
      StateHasChanged();
    }
    public async Task Refresh()
    {

      StateHasChanged();
    }
  }
}