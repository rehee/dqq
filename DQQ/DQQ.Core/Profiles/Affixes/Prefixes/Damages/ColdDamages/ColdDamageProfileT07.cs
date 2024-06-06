using DQQ.Attributes;
using DQQ.Enums;

namespace DQQ.Profiles.Affixes.Prefixes.Damages.ColdDamages
{
	[Pooled]
	public class ColdDamageProfileT07 : ColdDamageProfile
	{
		public override int AffixeLevel => 70;
		public override int TierLevel => 7;
		public override string? Name => "严寒";
		protected override EnumAffixeNumber number => EnumAffixeNumber.ColdDamageProfileT07;
		protected override int min => 67;
		protected override int max => 75;
	}
}
