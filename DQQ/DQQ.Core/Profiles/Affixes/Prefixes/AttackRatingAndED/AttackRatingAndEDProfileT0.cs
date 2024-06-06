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
	public class AttackRatingAndEDProfileT0 : AttackRatingAndEDProfile
	{
		public override int AffixeLevel => 0;
		public override int TierLevel => 0;
		public override AffixeRange[] Ranges => [
			AffixeRange.New(EnumPropertyType.AttackRating,1,9),
			AffixeRange.New(EnumPropertyType.DamageModifier,1,9)
		];
		public override EnumAffixeNumber ProfileNumber => EnumAffixeNumber.AttackRatingAndEDProfileT0;
		public override string? Name => "锋利";
	}
}
