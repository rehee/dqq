using DQQ.Attributes;
using DQQ.Enums;

namespace DQQ.Profiles.Affixes.Prefixes.Damages.FireDamages
{
	[Pooled]
	public class FireDamageProfileT03 : FireDamageProfile
	{
		public override int AffixeLevel => 30;
		public override int TierLevel => 3;

		public override string? Name => "烟雾";

		protected override EnumAffixeNumber number => EnumAffixeNumber.FireDamageProfileT03;

		protected override int min => 21;
		protected override int max => 31;
	}
}
