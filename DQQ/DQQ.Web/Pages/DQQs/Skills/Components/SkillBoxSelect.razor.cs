using DQQ.Components.Stages.Actors.Characters;
using Microsoft.AspNetCore.Components;

namespace DQQ.Web.Pages.DQQs.Skills.Components
{
	public class SkillBoxSelectPage : DQQPageBase
	{
		[Parameter]
		public Character? SelectedCharacter { get; set; }

		protected override void OnParametersSet()
		{
		
			base.OnParametersSet();
		}
	}
}