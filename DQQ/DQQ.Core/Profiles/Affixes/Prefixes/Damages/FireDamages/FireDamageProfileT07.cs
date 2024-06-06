using DQQ.Attributes;
using DQQ.Enums;

namespace DQQ.Profiles.Affixes.Prefixes.Damages.FireDamages
{
	[Pooled]
	public class FireDamageProfileT07 : FireDamageProfile
	{
		public override int AffixeLevel => 70;
		public override int TierLevel => 7;
		public override string? Name => "焚化";
		protected override EnumAffixeNumber number => EnumAffixeNumber.FireDamageProfileT07;
		protected override int min => 67;
		protected override int max => 75;
	}
}
