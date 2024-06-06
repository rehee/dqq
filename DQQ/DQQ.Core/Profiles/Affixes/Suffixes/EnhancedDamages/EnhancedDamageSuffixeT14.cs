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
	public class EnhancedDamageSuffixeT14 : EnhancedDamageSuffixe
	{
		public override int AffixeLevel => 70;
		public override int TierLevel => 14;
		public override EnumAffixeNumber ProfileNumber => EnumAffixeNumber.EnhancedDamageSuffixeT14;

		public override AffixeRange[] Ranges => [
			AffixeRange.New(EnumPropertyType.Damage, 150, 170)
			];

		public override string? Name => "测量";


	}
}
