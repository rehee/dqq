using DQQ.Attributes;
using DQQ.Enums;

namespace DQQ.Profiles.Affixes.Prefixes.Damages.LightningDamages
{
	[Pooled]
	public class LightningDamageProfileT04 : LightningDamageProfile
	{
		public override int AffixeLevel => 40;
		public override int TierLevel => 4;
		public override string? Name => "震撼";
		protected override EnumAffixeNumber number => EnumAffixeNumber.LightningDamageProfileT04;
		protected override int min => 31;
		protected override int max => 41;
	}
}
