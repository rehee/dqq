using DQQ.Attributes;
using DQQ.Enums;

namespace DQQ.Profiles.Affixes.Prefixes.Damages.ColdDamages
{
	[Pooled]
	public class ColdDamageProfileT06 : ColdDamageProfile
	{
		public override int AffixeLevel => 60;
		public override int TierLevel => 6;
		public override string? Name => "寒冬";
		protected override EnumAffixeNumber number => EnumAffixeNumber.ColdDamageProfileT06;
		protected override int min => 61;
		protected override int max => 66;
	}
}
