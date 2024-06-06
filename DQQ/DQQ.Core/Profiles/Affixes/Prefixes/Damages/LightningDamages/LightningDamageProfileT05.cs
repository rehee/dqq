using DQQ.Attributes;
using DQQ.Enums;

namespace DQQ.Profiles.Affixes.Prefixes.Damages.LightningDamages
{
	[Pooled]
	public class LightningDamageProfileT05 : LightningDamageProfile
	{
		public override int AffixeLevel => 50;
		public override int TierLevel => 5;
		public override string? Name => "脉冲";
		protected override EnumAffixeNumber number => EnumAffixeNumber.LightningDamageProfileT05;
		protected override int min => 41;
		protected override int max => 51;
	}
}
