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
	public class EnhancedDamageSuffixeT02 : EnhancedDamageSuffixe
	{
		public override int AffixeLevel => 10;
		public override int TierLevel => 2;
		public override EnumAffixeNumber ProfileNumber => EnumAffixeNumber.EnhancedDamageSuffixeT02;
		
		public override AffixeRange[] Ranges => [
			AffixeRange.New(EnumPropertyType.Damage, 6, 9)
			];

		public override string? Name => "狂怒";
		

	}
}
