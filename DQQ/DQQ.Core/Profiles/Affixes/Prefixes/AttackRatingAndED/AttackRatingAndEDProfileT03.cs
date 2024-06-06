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
	public class AttackRatingAndEDProfileT03 : AttackRatingAndEDProfile
	{
		public override int AffixeLevel => 19;
		public override int TierLevel => 3;
		public override AffixeRange[] Ranges => [
			AffixeRange.New(EnumPropertyType.AttackRating,41,60),
			AffixeRange.New(EnumPropertyType.DamageModifier,31,40)
		];
		public override EnumAffixeNumber ProfileNumber => EnumAffixeNumber.AttackRatingAndEDProfileT03;
		public override string? Name => "战士之";
	}
}
