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
	public class IncreasedLifeSuffixeT04: IncreasedLifeSuffixe
	{
		public override int AffixeLevel => 40;
		public override int TierLevel => 4;

		public override AffixeRange[] Ranges => [
			AffixeRange.New(EnumPropertyType.MaximunLife,40,49)
			];

		public override EnumAffixeNumber ProfileNumber => EnumAffixeNumber.IncreasedLifeSuffixeT04;

		public override string? Name => "强健的";
	}
}
