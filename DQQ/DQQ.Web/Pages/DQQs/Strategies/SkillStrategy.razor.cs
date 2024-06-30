using DQQ.Commons.DTOs;
using DQQ.Enums;
using DQQ.Services.StrategyServices;
using DQQ.Strategies.SkillStrategies;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace DQQ.Web.Pages.DQQs.Strategies
{
	public class SkillStrategyPage : DQQPageBase
	{
		[Parameter]
		public EnumSkillSlot? Slot { get; set; }

		public SkillDTO? SelectedDTO
		{
			get
			{
				if (SelectedCharacter?.SkillMap?.TryGetValue(Slot ?? EnumSkillSlot.NotSpecified, out var skill) == true)
				{
					return skill;
				}
				return null;
			}
		}

		public async Task CreateOrEditStrategy(SkillStrategyDTO? dto = null)
		{
			var newStrategy = dto ?? new SkillStrategyDTO();
			await dialogService.ShowComponent<SkillStrategyDetail>(
				new Dictionary<string, object?>()
				{
					["ParentRefreshEvent"] = ParentRefreshEvent,
					["SelectedCharacter"] = SelectedCharacter,
					["Slot"] = Slot,
					["StrategyDTO"] = newStrategy
				}, "", true);
		}
		[Inject]
		[NotNull]
		public IStrategyService? strategyService { get; set; }
		public async Task DeleteStrategy(SkillStrategyDTO? dto = null)
		{
			if (dto == null)
			{
				return;
			}

			await strategyService.SetActorSkillStrategy(SelectedCharacter?.DisplayId, Slot?? EnumSkillSlot.MainSlot, SelectedDTO?.SkillStrategies?.Where(b => b.Id != dto.Id));
			ParentRefreshEvent.InvokeEvent(this,EventArgs.Empty);
		}
	}
}