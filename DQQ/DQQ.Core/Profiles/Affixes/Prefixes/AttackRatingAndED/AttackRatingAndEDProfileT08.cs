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
	public class AttackRatingAndEDProfileT08 : AttackRatingAndEDProfile
	{
		public override int AffixeLevel => 70;
		public override int TierLevel => 8;
		public override AffixeRange[] Ranges => [
			AffixeRange.New(EnumPropertyType.AttackRating,151,250),
			AffixeRange.New(EnumPropertyType.DamageModifier,101,150)
		];
		public override EnumAffixeNumber ProfileNumber => EnumAffixeNumber.AttackRatingAndEDProfileT08;
		public override string? Name => "大师之";
	}
}
