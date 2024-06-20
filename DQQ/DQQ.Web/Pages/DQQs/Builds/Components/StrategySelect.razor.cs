using DQQ.Commons.DTOs;
using DQQ.Components.Stages.Actors.Characters;
using DQQ.Enums;
using DQQ.Strategies.SkillStrategies;
using Microsoft.AspNetCore.Components;

namespace DQQ.Web.Pages.DQQs.Builds.Components
{
	public class StrategySelectPage : DQQPageBase
	{
		[Parameter]
		public EnumSkillSlot? Slot { get; set; }
		public SkillDTO? SelectedSkillDTO => SelectedCharacter?.GetSelectedSkillDTO(Slot);

		public async Task Add()
		{
			await Task.CompletedTask;
			if (SelectedSkillDTO?.SkillStrategies == null && SelectedSkillDTO?.Profile != null)
			{
				SelectedSkillDTO!.SkillStrategies = new List<SkillStrategy>();
			}
			SelectedSkillDTO?.SkillStrategies?.Add(new DQQ.Strategies.SkillStrategies.SkillStrategy()
			{
				Priority = SelectedSkillDTO?.SkillStrategies?.Count() ?? 0
			});
			StateHasChanged();
		}
		public async Task Remove(DQQ.Strategies.SkillStrategies.SkillStrategy s)
		{
			await Task.CompletedTask;
			if (SelectedSkillDTO?.SkillStrategies?.Any() != true)
			{
				return;
			}
			var index = SelectedSkillDTO?.SkillStrategies.IndexOf(s) ?? -1;
			if (index < 0)
			{
				return;
			}
			SelectedSkillDTO?.SkillStrategies.Remove(s);
			var length = SelectedSkillDTO?.SkillStrategies?.Count ?? 0;
			for (var i = 0; i < length; i++)
			{
				SelectedSkillDTO!.SkillStrategies[i].Priority = i;
			}
			StateHasChanged();
		}
	}
}