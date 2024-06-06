using DQQ.Attributes;
using DQQ.Enums;

namespace DQQ.Profiles.Affixes.Prefixes.Damages.FireDamages
{
	[Pooled]
	public class FireDamageProfileT01 : FireDamageProfile
	{
		public override int AffixeLevel => 10;
		public override int TierLevel => 1;

		public override string? Name => "烈焰";

		protected override EnumAffixeNumber number => EnumAffixeNumber.FireDamageProfileT01;

		protected override int min => 6;
		protected override int max => 12;
	}
}
