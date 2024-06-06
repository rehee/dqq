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
	public class EnhancedDamageSuffixeT12 : EnhancedDamageSuffixe
	{
		public override int AffixeLevel => 60;
		public override int TierLevel => 12;
		public override EnumAffixeNumber ProfileNumber => EnumAffixeNumber.EnhancedDamageSuffixeT12;

		public override AffixeRange[] Ranges => [
			AffixeRange.New(EnumPropertyType.Damage, 60, 70)
			];

		public override string? Name => "价值";


	}
}
