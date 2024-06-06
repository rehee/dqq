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
	public class AttackRatingAndEDProfileT07 : AttackRatingAndEDProfile
	{
		public override int AffixeLevel => 56;
		public override int TierLevel => 7;
		public override AffixeRange[] Ranges => [
			AffixeRange.New(EnumPropertyType.AttackRating,121,150),
			AffixeRange.New(EnumPropertyType.DamageModifier,81,100)
		];
		public override EnumAffixeNumber ProfileNumber => EnumAffixeNumber.AttackRatingAndEDProfileT07;
		public override string? Name => "国王之";
	}
}
