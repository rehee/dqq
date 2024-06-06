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
	public class ArmorProfileT01 : ArmorProfile
	{
		public override int AffixeLevel => 5;
		public override int TierLevel => 1;
		public override AffixeRange[] Ranges => [
			AffixeRange.New(EnumPropertyType.Armor,10,30)
			];
		public override EnumAffixeNumber ProfileNumber => EnumAffixeNumber.ArmorProfileT01;

		public override string? Name => "结实的";
	}
}
