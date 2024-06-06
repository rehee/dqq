using DQQ.Attributes;
using DQQ.Enums;

namespace DQQ.Profiles.Affixes.Prefixes.Damages.LightningDamages
{
	[Pooled]
	public class LightningDamageProfileT06 : LightningDamageProfile
	{
		public override int AffixeLevel => 60;
		public override int TierLevel => 6;
		public override string? Name => "霹雳";
		protected override EnumAffixeNumber number => EnumAffixeNumber.LightningDamageProfileT06;
		protected override int min => 61;
		protected override int max => 66;
	}
}
