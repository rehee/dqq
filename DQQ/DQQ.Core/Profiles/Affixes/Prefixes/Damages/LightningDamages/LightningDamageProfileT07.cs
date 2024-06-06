using DQQ.Attributes;
using DQQ.Enums;

namespace DQQ.Profiles.Affixes.Prefixes.Damages.LightningDamages
{
	[Pooled]
	public class LightningDamageProfileT07 : LightningDamageProfile
	{
		public override int AffixeLevel => 70;
		public override int TierLevel => 7;
		public override string? Name => "雷霆";
		protected override EnumAffixeNumber number => EnumAffixeNumber.LightningDamageProfileT07;
		protected override int min => 67;
		protected override int max => 75;
	}
}
