using DQQ.Attributes;
using DQQ.Enums;

namespace DQQ.Profiles.Affixes.Suffixes.Lifes
{
	[Pooled]
	public class IncreasedLifeSuffixeT0 : IncreasedLifeSuffixe
	{
		public override int AffixeLevel => 0;
		public override int TierLevel => 0;

		public override AffixeRange[] Ranges => [
			AffixeRange.New(EnumPropertyType.MaximunLife,1,9)
			];

		public override EnumAffixeNumber ProfileNumber => EnumAffixeNumber.IncreasedLifeSuffixeT00;

		public override string? Name => "生命的";
	}
}
