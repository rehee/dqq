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
	public class EnhancedDamageSuffixeT19 : EnhancedDamageSuffixe
	{
		public override int AffixeLevel => 70;
		public override int TierLevel => 19;
		public override EnumAffixeNumber ProfileNumber => EnumAffixeNumber.EnhancedDamageSuffixeT19;

		public override AffixeRange[] Ranges => [
			AffixeRange.New(EnumPropertyType.Damage, 300, 330)
			];

		public override string? Name => "伟力";


	}
}
