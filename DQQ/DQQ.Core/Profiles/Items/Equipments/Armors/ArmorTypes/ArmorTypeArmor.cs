﻿using DQQ.Enums;
using DQQ.Profiles.Affixes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Items.Equipments.Armors.ArmorTypes
{
	public class ArmorTypeArmor : IArmorType
	{
		public AffixeRange[]? Ranges => [
			AffixeRange.New(EnumPropertyType.Armor,1,70, EnumAffixeRangeType.LevelBased,1m)
			];
	}
}
