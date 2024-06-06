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
	public class ArmorProfileT02 : ArmorProfile
	{
		public override int AffixeLevel => 9;
		public override int TierLevel => 2;
		public override AffixeRange[] Ranges => [
			AffixeRange.New(EnumPropertyType.ArmorPercentage,31,40)
			];
		public override EnumAffixeNumber ProfileNumber => EnumAffixeNumber.ArmorProfileT02;

		public override string? Name => "强壮的";
	}
}
