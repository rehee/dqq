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
	public class EnhancedDamageSuffixeT18 : EnhancedDamageSuffixe
	{
		public override int AffixeLevel => 70;
		public override int TierLevel => 18;
		public override EnumAffixeNumber ProfileNumber => EnumAffixeNumber.EnhancedDamageSuffixeT18;

		public override AffixeRange[] Ranges => [
			AffixeRange.New(EnumPropertyType.Damage, 270, 290)
			];

		public override string? Name => "超凡";


	}
}
