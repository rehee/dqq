using DQQ.Attributes;
using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Affixes.Prefixes.AttackRatingAndED
{
	[Pooled]
	public class AttackRatingAndEDProfileT02 : AttackRatingAndEDProfile
	{
		public override int AffixeLevel => 12;
		public override int TierLevel => 2;
		public override AffixeRange[] Ranges => [
			AffixeRange.New(EnumPropertyType.AttackRating,21,40),
			AffixeRange.New(EnumPropertyType.DamageModifier,21,30)
		];
		public override EnumAffixeNumber ProfileNumber => EnumAffixeNumber.AttackRatingAndEDProfileT02;
		public override string? Name => "精良";
	}
}
