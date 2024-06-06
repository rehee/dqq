using DQQ.Attributes;
using DQQ.Enums;

namespace DQQ.Profiles.Affixes.Prefixes.Damages.ColdDamages
{
	[Pooled]
	public class ColdDamageProfileT01 : ColdDamageProfile
	{
		public override int AffixeLevel => 10;
		public override int TierLevel => 1;

		public override string? Name => "雪";

		protected override EnumAffixeNumber number => EnumAffixeNumber.ColdDamageProfileT01;

		protected override int min => 6;
		protected override int max => 12;
	}
}
