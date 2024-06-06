using DQQ.Attributes;
using DQQ.Enums;

namespace DQQ.Profiles.Affixes.Prefixes.Damages.FireDamages
{
	[Pooled]
	public class FireDamageProfileT06 : FireDamageProfile
	{
		public override int AffixeLevel => 60;
		public override int TierLevel => 6;
		public override string? Name => "焚烧";
		protected override EnumAffixeNumber number => EnumAffixeNumber.FireDamageProfileT06;
		protected override int min => 61;
		protected override int max => 66;
	}
}
