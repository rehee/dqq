using DQQ.Attributes;
using DQQ.Enums;

namespace DQQ.Profiles.Affixes.Prefixes.Damages.ColdDamages
{
	[Pooled]
	public class ColdDamageProfileT0 : ColdDamageProfile
	{
		public override int AffixeLevel => 0;
		public override int TierLevel => 0;

		public override string? Name => "寒冷";

		protected override EnumAffixeNumber number => EnumAffixeNumber.ColdDamageProfileT0;

		protected override int min => 1;
		protected override int max => 5;
	}
}
