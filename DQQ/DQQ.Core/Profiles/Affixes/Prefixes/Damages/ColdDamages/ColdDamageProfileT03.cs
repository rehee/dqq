using DQQ.Attributes;
using DQQ.Enums;

namespace DQQ.Profiles.Affixes.Prefixes.Damages.ColdDamages
{
	[Pooled]
	public class ColdDamageProfileT03 : ColdDamageProfile
	{
		public override int AffixeLevel => 30;
		public override int TierLevel => 3;

		public override string? Name => "冰";

		protected override EnumAffixeNumber number => EnumAffixeNumber.ColdDamageProfileT03;

		protected override int min => 21;
		protected override int max => 31;
	}
}
