using DQQ.Components.Stages.Actors.Characters;
using Microsoft.AspNetCore.Components;

namespace DQQ.Web.Pages.DQQs.Builds
{
	public class SkillSlotListPage : DQQPageBase
	{
		[Parameter]
		public Character? SelectedCharacter { get; set; }

    protected override void OnParametersSet()
    {
      var d = ParentRefreshEvent;
      base.OnParametersSet();
    }
  }
}