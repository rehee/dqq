using DQQ.Attributes;
using DQQ.Enums;

namespace DQQ.Profiles.Affixes.Prefixes.Damages.LightningDamages
{
	[Pooled]
	public class LightningDamageProfileT03 : LightningDamageProfile
	{
		public override int AffixeLevel => 30;
		public override int TierLevel => 3;

		public override string? Name => "弧光";

		protected override EnumAffixeNumber number => EnumAffixeNumber.LightningDamageProfileT03;

		protected override int min => 21;
		protected override int max => 31;
	}
}
