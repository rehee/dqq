using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Helper
{
	public static class SkillSlotHelper
	{
		public static int MaxSkillNumber(this EnumSkillSlot? slot)
		{
			if (slot == null) return 0;
			switch (slot)
			{
				case EnumSkillSlot.MainSlot:
				case EnumSkillSlot.WeaponSlotTH:
					return 5;
				case EnumSkillSlot.WeaponSlot1:
				case EnumSkillSlot.WeaponSlot2:
					return 2;
				default:
					return 3;
			}
		}
	}
}
