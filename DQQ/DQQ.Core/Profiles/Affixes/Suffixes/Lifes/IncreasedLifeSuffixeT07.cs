using DQQ.Attributes;
using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Affixes.Suffixes.Lifes
{
	[Pooled]
	public class IncreasedLifeSuffixeT07: IncreasedLifeSuffixe
	{
		public override int AffixeLevel => 70;
		public override int TierLevel => 7;

		public override AffixeRange[] Ranges => [
			AffixeRange.New(EnumPropertyType.MaximunLife,70,79)
			];

		public override EnumAffixeNumber ProfileNumber => EnumAffixeNumber.IncreasedLifeSuffixeT07;

		public override string? Name => "耐力的";
	}
}
