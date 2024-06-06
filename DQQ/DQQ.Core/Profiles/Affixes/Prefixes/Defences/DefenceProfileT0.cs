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
	public class DefenceProfileT0 : DefenceProfile
	{
		public override int AffixeLevel => 0;
		public override int TierLevel => 0;
		public override AffixeRange[] Ranges => [
			AffixeRange.New(EnumPropertyType.DefencePercentage,1,9)
			];
		public override EnumAffixeNumber ProfileNumber => EnumAffixeNumber.DefenceProfileT0;

		public override string? Name => "厚实的";
	}
}
