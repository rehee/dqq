using DQQ.Attributes;
using DQQ.Enums;

namespace DQQ.Profiles.Affixes.Prefixes.Damages.FireDamages
{
	[Pooled]
	public class FireDamageProfileT05 : FireDamageProfile
	{
		public override int AffixeLevel => 50;
		public override int TierLevel => 5;
		public override string? Name => "浓缩";
		protected override EnumAffixeNumber number => EnumAffixeNumber.FireDamageProfileT05;
		protected override int min => 41;
		protected override int max => 51;
	}
}
