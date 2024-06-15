using DQQ.Commons.DTOs;
using DQQ.Enums;
using DQQ.TickLogs;
using Microsoft.AspNetCore.Components;

namespace DQQ.Web.Pages.DQQs.Combats.Components
{
	public partial class CombatPlaySwitch
	{
		[Parameter]
		public EnumCombatPlayType PlayType { get; set; }

		[Parameter]
		public Func<Task>? AfterCombatPlay { get; set; }
		[Parameter]
		public TickLogItem[]? CombatLog { get; set; }

		[Parameter]
		public CombatResultDTO? CombatResult { get; set; }

		public Task AfterCombatCallback()
		{
			if (AfterCombatPlay != null)
			{
				AfterCombatPlay();
			}
			return Task.CompletedTask;
		}
	}
}