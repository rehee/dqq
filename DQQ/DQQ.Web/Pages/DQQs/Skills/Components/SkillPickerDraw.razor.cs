using DQQ.Commons.DTOs;
using DQQ.Components.Stages.Actors.Characters;
using DQQ.Enums;
using DQQ.Web.Pages.DQQs.Builds.Components;
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

		public async Task SelectSkills(int? index = null)
		{
			await this.dialogService.ShowComponent<SkillSelector>(
				new Dictionary<string, object?>
				{
					["ParentRefreshEvent"]= ParentRefreshEvent,
					["SelectedCharacter"] = SelectedCharacter,
					["Slot"] = Slot,
					["CardTitle"] = "主动技能选择",
					["SupportSkillIndex"] = index,
					["BindingTypes"] = index == null ? new EnumSkillBindingType[] { EnumSkillBindingType.Active, EnumSkillBindingType.Trigger } : new EnumSkillBindingType[] { EnumSkillBindingType.Support, EnumSkillBindingType.Trigger, EnumSkillBindingType.Active }
				}
				);

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