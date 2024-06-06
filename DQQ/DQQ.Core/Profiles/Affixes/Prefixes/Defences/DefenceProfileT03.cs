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
	public class DefenceProfileT03 : DefenceProfile
	{
		public override int AffixeLevel => 30;
		public override int TierLevel => 3;
		public override AffixeRange[] Ranges => [
			AffixeRange.New(EnumPropertyType.DefencePercentage,41,50)
			];
		public override EnumAffixeNumber ProfileNumber => EnumAffixeNumber.DefenceProfileT03;

		public override string? Name => "光荣的";
	}
}
