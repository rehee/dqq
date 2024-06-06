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
	public class EnhancedDamageSuffixeT10 : EnhancedDamageSuffixe
	{
		public override int AffixeLevel => 50;
		public override int TierLevel => 10;
		public override EnumAffixeNumber ProfileNumber => EnumAffixeNumber.EnhancedDamageSuffixeT10;

		public override AffixeRange[] Ranges => [
			AffixeRange.New(EnumPropertyType.Damage, 46, 50)
			];

		public override string? Name => "杀戮";


	}
}
