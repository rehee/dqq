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
	public class DefenceProfileT05 : DefenceProfile
	{
		public override int AffixeLevel => 50;
		public override int TierLevel => 5;
		public override AffixeRange[] Ranges => [
			AffixeRange.New(EnumPropertyType.DefencePercentage,66,80)
			];
		public override EnumAffixeNumber ProfileNumber => EnumAffixeNumber.DefenceProfileT05;

		public override string? Name => "崇高的";
	}
}
