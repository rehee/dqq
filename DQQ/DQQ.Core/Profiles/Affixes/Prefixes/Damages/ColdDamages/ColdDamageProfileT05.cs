using DQQ.Attributes;
using DQQ.Enums;

namespace DQQ.Profiles.Affixes.Prefixes.Damages.ColdDamages
{
	[Pooled]
	public class ColdDamageProfileT05 : ColdDamageProfile
	{
		public override int AffixeLevel => 50;
		public override int TierLevel => 5;
		public override string? Name => "北风";
		protected override EnumAffixeNumber number => EnumAffixeNumber.ColdDamageProfileT05;
		protected override int min => 41;
		protected override int max => 51;
	}
}
