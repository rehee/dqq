using DQQ.Attributes;
using DQQ.Enums;

namespace DQQ.Profiles.Affixes.Prefixes.Damages.PhysicsDamages
{
	[Pooled]
	public class PhysicsDamageProfileT07 : PhysicsDamageProfile
	{
		public override int AffixeLevel => 70;
		public override int TierLevel => 7;
		public override string? Name => "灭绝的";
		protected override EnumAffixeNumber number => EnumAffixeNumber.PhysicsDamageProfileT07;
		protected override int min => 67;
		protected override int max => 75;
	}
}
