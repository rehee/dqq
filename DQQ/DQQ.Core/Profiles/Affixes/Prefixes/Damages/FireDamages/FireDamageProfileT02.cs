using DQQ.Attributes;
using DQQ.Enums;

namespace DQQ.Profiles.Affixes.Prefixes.Damages.FireDamages
{
	[Pooled]
	public class FireDamageProfileT02 : FireDamageProfile
	{
		public override int AffixeLevel => 20;
		public override int TierLevel => 2;

		public override string? Name => "闷烧";

		protected override EnumAffixeNumber number => EnumAffixeNumber.FireDamageProfileT02;

		protected override int min => 13;
		protected override int max => 20;
	}
}
