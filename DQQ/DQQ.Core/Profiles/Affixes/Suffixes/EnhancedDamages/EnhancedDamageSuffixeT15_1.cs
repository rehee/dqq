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
	public class EnhancedDamageSuffixeT15_1 : EnhancedDamageSuffixe
	{
		public override int AffixeLevel => 70;
		public override int TierLevel => 15;
		public override EnumAffixeNumber ProfileNumber => EnumAffixeNumber.EnhancedDamageSuffixeT15;

		public override AffixeRange[] Ranges => [
			AffixeRange.New(EnumPropertyType.Damage, 200, 210)
			];

		public override string? Name => "精灵";


	}
}
