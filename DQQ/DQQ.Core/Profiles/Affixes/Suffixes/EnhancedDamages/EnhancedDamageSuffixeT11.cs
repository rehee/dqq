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
	public class EnhancedDamageSuffixeT11 : EnhancedDamageSuffixe
	{
		public override int AffixeLevel => 55;
		public override int TierLevel => 11;
		public override EnumAffixeNumber ProfileNumber => EnumAffixeNumber.EnhancedDamageSuffixeT11;

		public override AffixeRange[] Ranges => [
			AffixeRange.New(EnumPropertyType.Damage, 51, 55)
			];

		public override string? Name => "宰杀";


	}
}
