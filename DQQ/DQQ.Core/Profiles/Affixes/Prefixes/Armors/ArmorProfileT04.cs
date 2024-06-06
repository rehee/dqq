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
	public class ArmorProfileT04 : ArmorProfile
	{
		public override int AffixeLevel => 25;
		public override int TierLevel => 4;
		public override AffixeRange[] Ranges => [
			AffixeRange.New(EnumPropertyType.ArmorPercentage,51,65)
			];
		public override EnumAffixeNumber ProfileNumber => EnumAffixeNumber.ArmorProfileT04;

		public override string? Name => "祝福的";
	}
}
