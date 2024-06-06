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
	public class AttackRatingAndEDProfileT04 : AttackRatingAndEDProfile
	{
		public override int AffixeLevel => 27;
		public override int TierLevel => 4;
		public override AffixeRange[] Ranges => [
			AffixeRange.New(EnumPropertyType.AttackRating,61,80),
			AffixeRange.New(EnumPropertyType.DamageModifier,51,65)
		];
		public override EnumAffixeNumber ProfileNumber => EnumAffixeNumber.AttackRatingAndEDProfileT04;
		public override string? Name => "士兵之";
	}
}
