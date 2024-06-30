using DQQ.Enums;
using Microsoft.AspNetCore.Components;

namespace DQQ.Web.Pages.DQQs.Skills.Components
{
	public class SkillAndStrategyWrapPage: DQQPageBase
	{
		[Parameter]
		public EnumSkillSlot? Slot { get; set; }
	}
}