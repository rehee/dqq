using DQQ.Profiles.Skills;
using Microsoft.AspNetCore.Components;

namespace DQQ.Web.Pages.DQQs.Skills.Components
{
	public class SkillComparePage: DQQPageBase
	{
		[Parameter]
		public int? SelectedIndex {  get; set; }

		[Parameter]
		public SkillProfile? CurrentSkill { get; set; }

		[Parameter]
		public SkillProfile? PickedSkill {  get; set; }

		[Parameter]
		public EventCallback ChangeSkill { get; set; }

		public Task OnChangeSKillClick()
		{
			if (ChangeSkill.HasDelegate)
			{
				ChangeSkill.InvokeAsync();
			}
			return Task.CompletedTask;
		}
	}
}