using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Affixes.Prefixes.Defences
{
	public abstract class DefenceProfile : PrefixProfile
	{
		public override EnumAffixeGroup AffixeGroup => EnumAffixeGroup.DefencePercentage;
		public override EnumEquipType[]? EquipTypeLimites => [EnumEquipType.BodyArmor];

		public override string? Discription => "防御率增加";
	}
}
