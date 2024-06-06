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
	public class EnhancedDamageSuffixeT04 : EnhancedDamageSuffixe
	{
		public override int AffixeLevel => 20;
		public override int TierLevel => 4;
		public override EnumAffixeNumber ProfileNumber => EnumAffixeNumber.EnhancedDamageSuffixeT04;
		
		public override AffixeRange[] Ranges => [
			AffixeRange.New(EnumPropertyType.Damage, 16, 20)
			];

		public override string? Name => "狂怒";
		

	}
}
