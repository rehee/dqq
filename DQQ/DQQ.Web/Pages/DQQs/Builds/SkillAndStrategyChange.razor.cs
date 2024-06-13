using BootstrapBlazor.Components;
using DQQ.Commons.DTOs;
using DQQ.Components.Stages.Actors;
using DQQ.Components.Stages.Actors.Characters;
using DQQ.Consts;
using DQQ.Enums;
using DQQ.Services.SkillServices;
using DQQ.Web.Pages.DQQs.Builds.Components;
using DQQ.Web.Pages.DQQs.Skills;
using Microsoft.AspNetCore.Components;
using ReheeCmf.Helpers;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Metrics;

namespace DQQ.Web.Pages.DQQs.Builds
{
	public class SkillAndStrategyChangePage : DQQPageBase
	{
		public Step? Step_1;

		[Parameter]
		public EnumSkillAndStrategy? Option { get; set; }
		

		[Parameter]
		public EnumSkillSlot? Slot { get; set; }
		[Parameter]
		public Character? SelectedCharacter { get; set; }



		[NotNull]
		public List<StepOption>? Items { get; set; }

		public bool IsLastStep { get; set; }

		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();
			var newOptions = new List<StepOption>();
			switch (Option)
			{
				case EnumSkillAndStrategy.All:
					newOptions.Add(
					new StepOption()
					{
						Text = WebConsts.ActiveSkillChooseText,
						Template = BootstrapDynamicComponent.CreateComponent<ActiveSkillSelect>
						(
							new Dictionary<string, object?>
							{
								["Slot"] = Slot,
								["SelectedCharacter"] = SelectedCharacter,
							}
						).Render()
					});
					newOptions.Add(
						new StepOption()
						{
							Text = WebConsts.SupportSkillChooseText,

							Template = BootstrapDynamicComponent.CreateComponent<SupportSkillSelect>
							(
								new Dictionary<string, object?>
								{
									["Slot"] = Slot,
									["SelectedCharacter"] = SelectedCharacter,
								}
							).Render()
						});
					newOptions.Add(
					new StepOption()
					{
						Text = WebConsts.StrategySkillChooseText,

						Template = BootstrapDynamicComponent.CreateComponent<StrategySelect>
						(
							new Dictionary<string, object?>
							{
								["Slot"] = Slot,
								["SelectedCharacter"] = SelectedCharacter,
							}
						).Render()
					});
					break;
				case EnumSkillAndStrategy.SkillAndSupport:
					newOptions.Add(
					new StepOption()
					{
						Text = WebConsts.ActiveSkillChooseText,
						Template = BootstrapDynamicComponent.CreateComponent<ActiveSkillSelect>
						(
							new Dictionary<string, object?>
							{
								["Slot"] = Slot,
								["SelectedCharacter"] = SelectedCharacter,
							}
						).Render()
					});
					newOptions.Add(
						new StepOption()
						{
							Text = WebConsts.SupportSkillChooseText,

							Template = BootstrapDynamicComponent.CreateComponent<SupportSkillSelect>
							(
								new Dictionary<string, object?>
								{
									["Slot"] = Slot,
									["SelectedCharacter"] = SelectedCharacter,
								}
							).Render()
						});
					break;
				case EnumSkillAndStrategy.SkillOnly:
					newOptions.Add(
					new StepOption()
					{
						Text = WebConsts.ActiveSkillChooseText,
						Template = BootstrapDynamicComponent.CreateComponent<ActiveSkillSelect>
						(
							new Dictionary<string, object?>
							{
								["Slot"] = Slot,
								["SelectedCharacter"] = SelectedCharacter,
							}
						).Render()
					});
					
					break;
				case EnumSkillAndStrategy.SupportOnly:
					newOptions.Add(
						new StepOption()
						{
							Text = WebConsts.SupportSkillChooseText,

							Template = BootstrapDynamicComponent.CreateComponent<SupportSkillSelect>
							(
								new Dictionary<string, object?>
								{
									["Slot"] = Slot,
									["SelectedCharacter"] = SelectedCharacter,
								}
							).Render()
						});
					break;
				case EnumSkillAndStrategy.StrategyOnly:
					newOptions.Add(
					new StepOption()
					{
						Text = WebConsts.StrategySkillChooseText,

						Template = BootstrapDynamicComponent.CreateComponent<StrategySelect>
						(
							new Dictionary<string, object?>
							{
								["Slot"] = Slot,
								["SelectedCharacter"] = SelectedCharacter,
							}
						).Render()
					});
					break;
			}
			
			
			
			Items = newOptions.ToList();
		}
		[NotNull]
		[Inject]
		public ISkillService? skillServices { get; set; }
		public async Task SaveDTO()
		{
			var dto = SelectedCharacter?.GetSelectedSkillDTO(Slot);
			
			var result = await skillServices.PickSkill(
				PickSkillDTO.New(dto, SelectedCharacter?.DisplayId, Slot));
			ParentRefreshEvent?.InvokeEvent(this, new EventArgs());
			await CloseDynamicDialog();
		}

		public void PrevStep(Step step)
		{
			step.Prev();
			IsLastStep = IsFinal(step);
			StateHasChanged();
		}

		public async Task NextStep(Step step)
		{

			await step.Next();
			IsLastStep = IsFinal(step);
			StateHasChanged();
		}

		public void ResetStep(Step step)
		{
			step.Reset();
			IsLastStep = IsFinal(step);
			StateHasChanged();
		}

		public bool IsFinal(Step step)
		{
			var mapper = step.GetMap();
			var result = mapper.PropertiesWithPrivate.FirstOrDefault(b => b.Name == "IsFinished")?.GetValue(step);
			if (result is bool rr)
			{
				return rr;
			}
			return false;
		}
	}
}