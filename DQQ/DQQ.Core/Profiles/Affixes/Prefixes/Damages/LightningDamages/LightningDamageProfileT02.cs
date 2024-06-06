using DQQ.Attributes;
using DQQ.Enums;

namespace DQQ.Profiles.Affixes.Prefixes.Damages.LightningDamages
{
	[Pooled]
	public class LightningDamageProfileT02 : LightningDamageProfile
	{
		public override int AffixeLevel => 20;
		public override int TierLevel => 2;

		public override string? Name => "嗡鸣";

		protected override EnumAffixeNumber number => EnumAffixeNumber.LightningDamageProfileT02;

		protected override int min => 13;
		protected override int max => 20;
	}
}
