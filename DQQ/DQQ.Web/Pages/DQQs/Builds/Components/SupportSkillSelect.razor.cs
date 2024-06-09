using DQQ.Components.Stages.Actors.Characters;
using DQQ.Enums;
using Microsoft.AspNetCore.Components;

namespace DQQ.Web.Pages.DQQs.Builds.Components
{
	public class SupportSkillSelectPage : DQQPageBase
	{
		[Parameter]
		public EnumSkillSlot? Slot { get; set; }
		[Parameter]
		public Character? SelectedCharacter { get; set; }

		public int SkillLimit => Slot.MaxSkillNumber();
	}
}