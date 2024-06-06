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
	public class AttackRatingAndEDProfileT01 : AttackRatingAndEDProfile
	{
		public override int AffixeLevel => 5;
		public override int TierLevel => 1;
		public override AffixeRange[] Ranges => [
			AffixeRange.New(EnumPropertyType.AttackRating,10,20),
			AffixeRange.New(EnumPropertyType.DamageModifier,10,20)
		];
		public override EnumAffixeNumber ProfileNumber => EnumAffixeNumber.AttackRatingAndEDProfileT01;
		public override string? Name => "尖锐";
	}
}
