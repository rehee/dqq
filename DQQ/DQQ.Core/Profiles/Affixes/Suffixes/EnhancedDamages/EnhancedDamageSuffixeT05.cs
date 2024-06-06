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
	public class EnhancedDamageSuffixeT05 : EnhancedDamageSuffixe
	{
		public override int AffixeLevel => 25;
		public override int TierLevel => 5;
		public override EnumAffixeNumber ProfileNumber => EnumAffixeNumber.EnhancedDamageSuffixeT05;
		
		public override AffixeRange[] Ranges => [
			AffixeRange.New(EnumPropertyType.Damage, 21, 25)
			];

		public override string? Name => "伤残";
		

	}
}
