using DQQ.Commons.DTOs;
using DQQ.Components.Stages.Actors.Characters;
using DQQ.Enums;
using Microsoft.AspNetCore.Components;

namespace DQQ.Web.Pages.DQQs.Skills.Components
{
	public class SkillPickerDrawPage : DQQPageBase
	{
		public bool IsOpen { get; set; }

		public Task OpenDrawer()
		{
			IsOpen = !IsOpen;
			StateHasChanged();
			return Task.CompletedTask;
		}
		[Parameter]
		public EnumSkillSlot? Slot { get; set; }

		[Parameter]
		public Character? SelectedCharacter { get; set; }

		public async Task SelectSkills()
		{
			await Task.Delay(3000);

			IsOpen = false;
			StateHasChanged();
		}

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
	}
}