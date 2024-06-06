using DQQ.Attributes;
using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Affixes.Prefixes.Armors
{
	[Pooled]
	public class ArmorProfileT03 : ArmorProfile
	{
		public override int AffixeLevel => 19;
		public override int TierLevel => 3;
		public override AffixeRange[] Ranges => [
			AffixeRange.New(EnumPropertyType.ArmorPercentage,41,50)
			];
		public override EnumAffixeNumber ProfileNumber => EnumAffixeNumber.ArmorProfileT03;

		public override string? Name => "光荣的";
	}
}
