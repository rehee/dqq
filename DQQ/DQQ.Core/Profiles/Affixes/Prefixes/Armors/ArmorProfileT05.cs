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
	public class ArmorProfileT05 : ArmorProfile
	{
		public override int AffixeLevel => 31;
		public override int TierLevel => 5;
		public override AffixeRange[] Ranges => [
			AffixeRange.New(EnumPropertyType.ArmorPercentage,66,80)
			];
		public override EnumAffixeNumber ProfileNumber => EnumAffixeNumber.ArmorProfileT05;

		public override string? Name => "崇高的";
	}
}
