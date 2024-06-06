using DQQ.Attributes;
using DQQ.Enums;
using DQQ.Profiles.Affixes.Prefixes.Armors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Affixes.Prefixes.Armors
{
	[Pooled]
	public class ArmorProfileT0 : ArmorProfile
	{
		public override int AffixeLevel => 0;
		public override int TierLevel => 0;
		public override AffixeRange[] Ranges => [
			AffixeRange.New(EnumPropertyType.ArmorPercentage,1,9)
			];
		public override EnumAffixeNumber ProfileNumber => EnumAffixeNumber.ArmorProfileT0;

		public override string? Name => "厚实的";
	}
}
