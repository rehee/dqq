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
	public class IncreasedLifeSuffixeT08: IncreasedLifeSuffixe
	{
		public override int AffixeLevel => 70;
		public override int TierLevel => 8;

		public override AffixeRange[] Ranges => [
			AffixeRange.New(EnumPropertyType.MaximunLife,80,89)
			];

		public override EnumAffixeNumber ProfileNumber => EnumAffixeNumber.IncreasedLifeSuffixeT08;

		public override string? Name => "耐久的";
	}
}
