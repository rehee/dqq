using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Affixes.Prefixes.Armors
{
	public abstract class ArmorProfile : PrefixProfile
	{
		public override EnumAffixeGroup AffixeGroup => EnumAffixeGroup.ArmorPercentage;
		public override EnumEquipType[]? EquipTypeLimites => [
			EnumEquipType.BodyArmor,EnumEquipType.Helmet,EnumEquipType.Boots,EnumEquipType.Glove
			];

		public override string? Discription => "护甲增加";
	}
}
