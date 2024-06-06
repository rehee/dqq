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
	public class AttackRatingAndEDProfileT06 : AttackRatingAndEDProfile
	{
		public override int AffixeLevel => 47;
		public override int TierLevel => 6;
		public override AffixeRange[] Ranges => [
			AffixeRange.New(EnumPropertyType.AttackRating,101,120),
			AffixeRange.New(EnumPropertyType.DamageModifier,66,80)
		];
		public override EnumAffixeNumber ProfileNumber => EnumAffixeNumber.AttackRatingAndEDProfileT06;
		public override string? Name => "君王之";
	}
}
