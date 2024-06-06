using DQQ.Attributes;
using DQQ.Enums;

namespace DQQ.Profiles.Affixes.Prefixes.Damages.LightningDamages
{
	[Pooled]
	public class LightningDamageProfileT0 : LightningDamageProfile
	{
		public override int AffixeLevel => 0;
		public override int TierLevel => 0;

		public override string? Name => "静电";

		protected override EnumAffixeNumber number => EnumAffixeNumber.LightningDamageProfileT0;

		protected override int min => 1;
		protected override int max => 5;
	}
}
