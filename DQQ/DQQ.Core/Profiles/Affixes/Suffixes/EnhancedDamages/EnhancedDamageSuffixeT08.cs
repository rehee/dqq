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
	public class EnhancedDamageSuffixeT08 : EnhancedDamageSuffixe
	{
		public override int AffixeLevel => 40;
		public override int TierLevel => 8;
		public override EnumAffixeNumber ProfileNumber => EnumAffixeNumber.EnhancedDamageSuffixeT08;

		public override AffixeRange[] Ranges => [
			AffixeRange.New(EnumPropertyType.Damage, 36, 40)
			];

		public override string? Name => "凝血";


	}
}
