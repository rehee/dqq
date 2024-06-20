using DQQ.Attributes;
using DQQ.Components.Stages.Actors.Characters;
using DQQ.Enums;
using DQQ.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.ZProgress
{
	[Pooled]
	public class CombatProgress : ProgressProfile
	{
		public override EnumProgress ProfileNumber => EnumProgress.CombatProgress;

		public override string? Name => "战斗";

		public override string? Discription => "战斗";

		public override bool AvaliableCheck(Character? character)
		{
			return character?.Skills?.Any() == true && character?.Skills?.Where(b =>
			{
				if (b.SkillProfile?.IsPlayerAvaliableSkill(character) != true)
				{
					return false;
				}
				switch (b.Slot)
				{
					case EnumSkillSlot.WeaponSlotTH: return EnumProgress.SkillSlotW2H.IsUnlocked(character);
					case EnumSkillSlot.WeaponSlot1: return EnumProgress.SkillSlotW1H1.IsUnlocked(character);
					case EnumSkillSlot.WeaponSlot2: return EnumProgress.SkillSlotW1H2.IsUnlocked(character);
					case EnumSkillSlot.GeneralSlot1: return EnumProgress.SkillSlotGeneral1.IsUnlocked(character);
					case EnumSkillSlot.GeneralSlot2: return EnumProgress.SkillSlotGeneral2.IsUnlocked(character);
					case EnumSkillSlot.GeneralSlot3: return EnumProgress.SkillSlotGeneral3.IsUnlocked(character);
					case EnumSkillSlot.MainSlot: return true;
				}
				return false;
			})?.Any() == true;
		}
	}
}
