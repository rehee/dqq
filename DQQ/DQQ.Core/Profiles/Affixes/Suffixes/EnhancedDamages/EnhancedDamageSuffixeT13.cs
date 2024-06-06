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
	public class EnhancedDamageSuffixeT13 : EnhancedDamageSuffixe
	{
		public override int AffixeLevel => 65;
		public override int TierLevel => 13;
		public override EnumAffixeNumber ProfileNumber => EnumAffixeNumber.EnhancedDamageSuffixeT13;

		public override AffixeRange[] Ranges => [
			AffixeRange.New(EnumPropertyType.Damage, 80, 100)
			];

		public override string? Name => "快乐";


	}
}
