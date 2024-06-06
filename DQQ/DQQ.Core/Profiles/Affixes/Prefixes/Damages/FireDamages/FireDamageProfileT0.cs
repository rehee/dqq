using DQQ.Attributes;
using DQQ.Enums;

namespace DQQ.Profiles.Affixes.Prefixes.Damages.FireDamages
{
	[Pooled]
	public class FireDamageProfileT0 : FireDamageProfile
	{
		public override int AffixeLevel => 0;
		public override int TierLevel => 0;

		public override string? Name => "灰烬";

		protected override EnumAffixeNumber number => EnumAffixeNumber.FireDamageProfileT0;

		protected override int min => 1;
		protected override int max => 5;
	}
}
