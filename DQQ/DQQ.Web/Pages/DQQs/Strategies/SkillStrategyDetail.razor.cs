using DQQ.Commons.DTOs;
using DQQ.Enums;
using DQQ.Services.StrategyServices;
using DQQ.Strategies.SkillStrategies;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace DQQ.Web.Pages.DQQs.Strategies
{
	public class SkillStrategyDetailPage : DQQPageBase
	{
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

		[Parameter]
		[NotNull]
		public SkillStrategyDTO? StrategyDTO { get; set; }

		[Parameter]
		public EnumSkillSlot? Slot { get; set; }

		[Parameter]
		public bool Readonly {  get; set; }
		[Parameter]
		public bool IsEdit {  get; set; }

		

		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();
			if (StrategyDTO.CastCondition == null)
			{
				StrategyDTO.UsePreset = true;
				StrategyDTO.CastCondition = new SkillCastConditionDTO();
			}
			if (StrategyDTO.SkillTarget == null)
			{
				StrategyDTO.SkillTarget = new SkillTargetDTO();
			}		
		}

		[Inject]
		[NotNull]
		public IStrategyService? StrategyService { get; set; }

		public override async Task<bool> SaveFunction()
		{
			await base.SaveFunction();

			if (SelectedDTO == null)
			{
				return true;
			}
			var strategy = SelectedDTO?.SkillStrategies?.Where(b=>b.Id!= StrategyDTO.Id)?.ToList()?? new List<SkillStrategyDTO>();
			strategy.Add(StrategyDTO);
			await StrategyService.SetActorSkillStrategy(SelectedCharacter?.DisplayId,Slot?? EnumSkillSlot.MainSlot,strategy);
			ParentRefreshEvent.InvokeEvent(this, EventArgs.Empty);
			return true;
		}
	}
}