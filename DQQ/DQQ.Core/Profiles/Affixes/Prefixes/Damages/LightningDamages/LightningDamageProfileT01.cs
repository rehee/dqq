using DQQ.Attributes;
using DQQ.Enums;

namespace DQQ.Profiles.Affixes.Prefixes.Damages.LightningDamages
{
	[Pooled]
	public class LightningDamageProfileT01 : LightningDamageProfile
	{
		public override int AffixeLevel => 10;
		public override int TierLevel => 1;

		public override string? Name => "发光";

		protected override EnumAffixeNumber number => EnumAffixeNumber.LightningDamageProfileT01;

		protected override int min => 6;
		protected override int max => 12;
	}
}
