using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Affixes.Prefixes.Damages
{
	public abstract class DamageProfile : PrefixProfile
	{
		public override EnumAffixeGroup AffixeGroup => EnumAffixeGroup.IncreasedDamage;
		protected abstract EnumPropertyType propertyType { get; }
		protected abstract EnumAffixeNumber number { get; }
		protected abstract string discription { get; }
		protected abstract int min { get; }
		protected abstract int max { get; }
		public override AffixeRange[] Ranges => [
			AffixeRange.New(propertyType,min,max)
			];
		public override EnumEquipType[]? EquipTypeLimites => [
			EnumEquipType.MainHandWeapon,
			EnumEquipType.OneHandWeapon,
			EnumEquipType.TwoHandWeapon,
			EnumEquipType.Helmet,
			EnumEquipType.Glove,
			EnumEquipType.Ring,
			EnumEquipType.Amulet
		];
		public override EnumAffixeNumber ProfileNumber => number;
		public override string? Discription => $"{discription}伤害增加";
	}
}
