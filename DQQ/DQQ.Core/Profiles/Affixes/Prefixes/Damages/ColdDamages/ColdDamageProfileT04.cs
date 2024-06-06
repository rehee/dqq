using DQQ.Attributes;
using DQQ.Enums;

namespace DQQ.Profiles.Affixes.Prefixes.Damages.ColdDamages
{
	[Pooled]
	public class ColdDamageProfileT04 : ColdDamageProfile
	{
		public override int AffixeLevel => 40;
		public override int TierLevel => 4;
		public override string? Name => "颤抖";
		protected override EnumAffixeNumber number => EnumAffixeNumber.ColdDamageProfileT04;
		protected override int min => 31;
		protected override int max => 41;
	}
}
