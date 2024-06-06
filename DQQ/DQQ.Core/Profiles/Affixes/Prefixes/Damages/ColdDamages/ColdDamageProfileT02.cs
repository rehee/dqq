using DQQ.Attributes;
using DQQ.Enums;

namespace DQQ.Profiles.Affixes.Prefixes.Damages.ColdDamages
{
	[Pooled]
	public class ColdDamageProfileT02 : ColdDamageProfile
	{
		public override int AffixeLevel => 20;
		public override int TierLevel => 2;

		public override string? Name => "冰";

		protected override EnumAffixeNumber number => EnumAffixeNumber.ColdDamageProfileT02;

		protected override int min => 13;
		protected override int max => 20;
	}
}
