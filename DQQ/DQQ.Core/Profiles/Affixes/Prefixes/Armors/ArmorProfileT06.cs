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
	public class ArmorProfileT06 : ArmorProfile
	{
		public override int AffixeLevel => 36;
		public override int TierLevel => 6;
		public override AffixeRange[] Ranges => [
			AffixeRange.New(EnumPropertyType.ArmorPercentage,81,100)
			];
		public override EnumAffixeNumber ProfileNumber => EnumAffixeNumber.ArmorProfileT06;

		public override string? Name => "神圣的";
	}
}
