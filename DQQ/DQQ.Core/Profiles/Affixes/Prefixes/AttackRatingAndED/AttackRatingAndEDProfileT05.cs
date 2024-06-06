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
	public class AttackRatingAndEDProfileT05 : AttackRatingAndEDProfile
	{
		public override int AffixeLevel => 38;
		public override int TierLevel => 5;
		public override AffixeRange[] Ranges => [
			AffixeRange.New(EnumPropertyType.AttackRating,81,100),
			AffixeRange.New(EnumPropertyType.DamageModifier,51,65)
		];
		public override EnumAffixeNumber ProfileNumber => EnumAffixeNumber.AttackRatingAndEDProfileT05;
		public override string? Name => "骑士之";
	}
}
