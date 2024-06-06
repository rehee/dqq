using DQQ.Attributes;
using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Affixes.Prefixes.Defences
{
	[Pooled]
	public class DefenceProfileT04 : DefenceProfile
	{
		public override int AffixeLevel => 40;
		public override int TierLevel => 4;
		public override AffixeRange[] Ranges => [
			AffixeRange.New(EnumPropertyType.DefencePercentage,51,65)
			];
		public override EnumAffixeNumber ProfileNumber => EnumAffixeNumber.DefenceProfileT04;

		public override string? Name => "祝福的";
	}
}
