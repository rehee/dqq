using DQQ.Components.Stages.Actors.Characters;
using Microsoft.AspNetCore.Components;

namespace DQQ.Web.Pages.DQQs.Builds
{
	public class SkillSlotListPage : DQQPageBase
	{
    protected override void OnParametersSet()
    {
      var d = ParentRefreshEvent;
      base.OnParametersSet();
    }
  }
}