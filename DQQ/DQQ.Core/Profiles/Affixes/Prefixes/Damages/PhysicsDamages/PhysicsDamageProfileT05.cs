using DQQ.Attributes;
using DQQ.Enums;

namespace DQQ.Profiles.Affixes.Prefixes.Damages.PhysicsDamages
{
	[Pooled]
	public class PhysicsDamageProfileT05 : PhysicsDamageProfile
	{
		public override int AffixeLevel => 50;
		public override int TierLevel => 5;
		public override string? Name => "破坏的";
		protected override EnumAffixeNumber number => EnumAffixeNumber.PhysicsDamageProfileT05;
		protected override int min => 41;
		protected override int max => 51;
	}
}
