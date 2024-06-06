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
	public class EnhancedDamageSuffixeT01 : EnhancedDamageSuffixe
	{
		public override int AffixeLevel => 5;
		public override int TierLevel => 1;
		public override EnumAffixeNumber ProfileNumber => EnumAffixeNumber.EnhancedDamageSuffixeT01;
		
		public override AffixeRange[] Ranges => [
			AffixeRange.New(EnumPropertyType.Damage, 2, 5)
			];

		public override string? Name => "愤怒";
		

	}
}
