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
	public class EnhancedDamageSuffixeT06 : EnhancedDamageSuffixe
	{
		public override int AffixeLevel => 30;
		public override int TierLevel => 6;
		public override EnumAffixeNumber ProfileNumber => EnumAffixeNumber.EnhancedDamageSuffixeT06;
		
		public override AffixeRange[] Ranges => [
			AffixeRange.New(EnumPropertyType.Damage, 26, 30)
			];

		public override string? Name => "屠杀";
		

	}
}
