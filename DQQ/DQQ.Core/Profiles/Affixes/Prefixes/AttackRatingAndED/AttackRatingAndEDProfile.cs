using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Affixes.Prefixes.AttackRatingAndED
{
	public abstract class AttackRatingAndEDProfile : PrefixProfile
	{
		public override EnumAffixeGroup AffixeGroup => EnumAffixeGroup.AttackRatingAndED;
		public override EnumEquipType[]? EquipTypeLimites => [EnumEquipType.OneHandWeapon, EnumEquipType.MainHandWeapon, EnumEquipType.TwoHandWeapon];
		public override EnumRarity[]? RarityLimits => [EnumRarity.Rare];
		public override string? Discription => "准确率与伤害增加";
	}
}
