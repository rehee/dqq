using DQQ.Commons.DTOs;
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
		public SkillDTO? SelectedSkill => SelectedCharacter?.GetSelectedSkillDTO(Slot);
		public int SkillLimit => Slot.MaxSkillNumber();
		public SkillDTO[]? SupportSkills => SelectedSkill?.SupportSkills;

		public bool[] IsBackdropOpens { get; set; } = { false, false, false, false, false, false };

		public async Task OpenDrawer(int index)
		{
			await Task.CompletedTask;
			IsBackdropOpens[index] = true;
		}
		protected override async Task OnParametersSetAsync()
		{
			await base.OnParametersSetAsync();
			if (SelectedSkill != null)
			{
				var emptySkill = new[] {
					SkillDTO.New(EnumSkill.NotSpecified),
					SkillDTO.New(EnumSkill.NotSpecified),
					SkillDTO.New(EnumSkill.NotSpecified),
					SkillDTO.New(EnumSkill.NotSpecified),
					SkillDTO.New(EnumSkill.NotSpecified),
					SkillDTO.New(EnumSkill.NotSpecified) };
				if (SelectedSkill.SupportSkills == null)
				{
					SelectedSkill.SupportSkills = emptySkill;
				}
				else
				{
					SelectedSkill.SupportSkills = SelectedSkill.SupportSkills.Concat(emptySkill).Take(5).ToArray();
				}
			}

		}
	}
}