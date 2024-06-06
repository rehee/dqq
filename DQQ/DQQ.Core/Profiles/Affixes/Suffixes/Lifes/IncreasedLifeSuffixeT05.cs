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
	public class IncreasedLifeSuffixeT05: IncreasedLifeSuffixe
	{
		public override int AffixeLevel => 50;
		public override int TierLevel => 5;

		public override AffixeRange[] Ranges => [
			AffixeRange.New(EnumPropertyType.MaximunLife,50,59)
			];

		public override EnumAffixeNumber ProfileNumber => EnumAffixeNumber.IncreasedLifeSuffixeT05;

		public override string? Name => "结实的";
	}
}
