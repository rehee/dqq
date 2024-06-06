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
	public class DefenceProfileT01 : DefenceProfile
	{
		public override int AffixeLevel => 10;
		public override int TierLevel => 1;
		public override AffixeRange[] Ranges => [
			AffixeRange.New(EnumPropertyType.DefencePercentage,10,30)
			];
		public override EnumAffixeNumber ProfileNumber => EnumAffixeNumber.DefenceProfileT01;

		public override string? Name => "结实的";
	}
}
