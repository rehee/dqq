using DQQ.Attributes;
using DQQ.Enums;

namespace DQQ.Profiles.Affixes.Prefixes.Damages.PhysicsDamages
{
	[Pooled]
	public class PhysicsDamageProfileT04 : PhysicsDamageProfile
	{
		public override int AffixeLevel => 40;
		public override int TierLevel => 4;
		public override string? Name => "无情的";
		protected override EnumAffixeNumber number => EnumAffixeNumber.PhysicsDamageProfileT04;
		protected override int min => 31;
		protected override int max => 41;
	}
}
