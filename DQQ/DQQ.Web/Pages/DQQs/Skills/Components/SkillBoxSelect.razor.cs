using DQQ.Components.Stages.Actors.Characters;
using DQQ.Enums;
using Microsoft.AspNetCore.Components;

namespace DQQ.Web.Pages.DQQs.Skills.Components
{
	public class SkillBoxSelectPage : DQQPageBase
	{
		protected override void OnParametersSet()
		{
		
			base.OnParametersSet();
		}
		public bool IsUnlock(EnumSkillSlot? slot)
		{
			if (slot == null || SelectedCharacter == null)
			{
				return false;
			}
			switch (slot)
			{
				case EnumSkillSlot.MainSlot: return true;
				case EnumSkillSlot.WeaponSlotTH: return SelectedCharacter.WithTwoHandWeapon && EnumProgress.SkillSlotW2H.IsUnlocked(SelectedCharacter);
				case EnumSkillSlot.WeaponSlot1: return SelectedCharacter.WithWeapon1 && EnumProgress.SkillSlotW1H1.IsUnlocked(SelectedCharacter);
				case EnumSkillSlot.WeaponSlot2: return SelectedCharacter.WithWeapon2 && EnumProgress.SkillSlotW1H2.IsUnlocked(SelectedCharacter);
				case EnumSkillSlot.GeneralSlot1: return EnumProgress.SkillSlotGeneral1.IsUnlocked(SelectedCharacter);
				case EnumSkillSlot.GeneralSlot2: return  EnumProgress.SkillSlotGeneral2.IsUnlocked(SelectedCharacter);
				case EnumSkillSlot.GeneralSlot3: return EnumProgress.SkillSlotGeneral3.IsUnlocked(SelectedCharacter);
			}

			return false;
		}

		public string? SlotSkillName(EnumSkillSlot? slot,string? defaultValue=null)
		{
			if(SelectedCharacter?.SkillMap?.TryGetValue(slot ?? EnumSkillSlot.NotSpecified, out var dto)==true)
			{
				return dto?.Profile?.Name?? defaultValue;
			}
			return defaultValue;
		}

	}
}