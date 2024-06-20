using DQQ.Enums;
using Microsoft.AspNetCore.Components;

namespace DQQ.Web.Pages.DQQs.Combats
{
	public class CombatPagePage : DQQPageBase
	{
		[Parameter]
		public EnumMapNumber? SelectedMap { get; set; }
	}
}