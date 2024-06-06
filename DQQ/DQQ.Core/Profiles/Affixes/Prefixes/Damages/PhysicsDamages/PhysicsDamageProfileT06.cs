using DQQ.Attributes;
using DQQ.Enums;

namespace DQQ.Profiles.Affixes.Prefixes.Damages.PhysicsDamages
{
	[Pooled]
	public class PhysicsDamageProfileT06 : PhysicsDamageProfile
	{
		public override int AffixeLevel => 60;
		public override int TierLevel => 6;
		public override string? Name => "毁灭的";
		protected override EnumAffixeNumber number => EnumAffixeNumber.PhysicsDamageProfileT06;
		protected override int min => 61;
		protected override int max => 66;
	}
}
