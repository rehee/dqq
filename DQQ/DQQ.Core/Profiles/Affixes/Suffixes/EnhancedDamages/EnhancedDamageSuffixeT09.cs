using DQQ.Attributes;
using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Affixes.Suffixes.EnhancedDamages
{
	[Pooled]
	public class EnhancedDamageSuffixeT09 : EnhancedDamageSuffixe
	{
		public override int AffixeLevel => 45;
		public override int TierLevel => 8;
		public override EnumAffixeNumber ProfileNumber => EnumAffixeNumber.EnhancedDamageSuffixeT09;

		public override AffixeRange[] Ranges => [
			AffixeRange.New(EnumPropertyType.Damage, 41, 45)
			];

		public override string? Name => "残杀";


	}
}
