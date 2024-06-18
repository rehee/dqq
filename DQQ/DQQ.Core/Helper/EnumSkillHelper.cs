using DQQ.Entities;
using DQQ.Enums;
using DQQ.Pools;
using DQQ.Profiles;
using DQQ.Profiles.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Helper
{
	public static class EnumSkillHelper
	{
		public static bool SlotCheck(this SkillEntity skill, bool? with2H = null, bool? withH1 = null, bool? withH2 = null)
		{
			if (skill.Slot == EnumSkillSlot.NotSpecified)
			{
				return false;
			}
			var generalSlot = new EnumSkillSlot[] {
				EnumSkillSlot.MainSlot,
				EnumSkillSlot.GeneralSlot1,
				EnumSkillSlot.GeneralSlot2,
				EnumSkillSlot.GeneralSlot3 };
			if (generalSlot.Contains(skill.Slot))
			{
				return true;
			}
			if (with2H == true)
			{
				return skill.Slot == EnumSkillSlot.WeaponSlotTH;
			}
			var oneHands = new EnumSkillSlot[] { EnumSkillSlot.WeaponSlot1, EnumSkillSlot.WeaponSlot2 };
			return oneHands.Contains(skill.Slot);
		}
	}
}