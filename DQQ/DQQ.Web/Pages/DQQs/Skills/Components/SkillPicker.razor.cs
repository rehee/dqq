using BootstrapBlazor.Components;
using DQQ.Commons.DTOs;
using DQQ.Components.Stages.Actors.Characters;
using DQQ.Enums;
using DQQ.Web.Pages.DQQs.Builds;
using Microsoft.AspNetCore.Components;

namespace DQQ.Web.Pages.DQQs.Skills.Components
{
	public class SkillPickerPage : DQQPageBase
	{
		[Parameter]
		public EnumSkillSlot? Slot { get; set; }

		[Parameter]
		public Character? SelectedCharacter { get; set; }

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

		protected override async Task OnParametersSetAsync()
		{
			await base.OnParametersSetAsync();
			if (Slot == null || SelectedCharacter == null)
			{
				return;
			}
			if (SelectedCharacter?.SkillMap?.TryGetValue(Slot.Value, out var skill) == true)
			{

			}
			else
			{
				SelectedCharacter?.SkillMap?.TryAdd(Slot.Value, new Commons.DTOs.SkillDTO
				{
					SkillNumber = EnumSkillNumber.NotSpecified,
				});
			}
		}



		public bool Avaliable
		{
			get
			{
				if (Slot == null || SelectedCharacter == null)
				{
					return false;
				}
				switch (Slot)
				{
					case EnumSkillSlot.WeaponSlotTH: return SelectedCharacter?.WithTwoHandWeapon == true;
					case EnumSkillSlot.WeaponSlot1: return SelectedCharacter?.WithWeapon1 == true;
					case EnumSkillSlot.WeaponSlot2: return SelectedCharacter?.WithWeapon2 == true;
					default: return true;
				}
			}
		}

		public Color SkillBordColor => Avaliable ? Color.None : Color.Danger;


		public EnumSkillAndStrategy OptionSelected { get; set; }
		public async Task SelectedItemChanged(SelectedItem item)
		{
			Enum.TryParse<EnumSkillAndStrategy>(item.Value, out var option);
			await SlotSelectOpen(option);
		}

		public async Task SlotSelectOpen(EnumSkillAndStrategy? options)
		{
			var op = options ?? OptionSelected;
			if (options != null)
			{
				OptionSelected = options.Value;
			}
			await dialogService.ShowComponent<SkillAndStrategyChange>(
			new Dictionary<string, object?>
			{
				["ParentRefreshEvent"] = ParentRefreshEvent,
				["SelectedCharacter"] = SelectedCharacter,
				["Option"] = op,
				["Slot"] = Slot
			}
			, "");
		}
	}
}