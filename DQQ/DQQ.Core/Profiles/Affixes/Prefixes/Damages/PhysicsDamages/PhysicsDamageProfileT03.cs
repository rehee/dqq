using DQQ.Attributes;
using DQQ.Enums;

namespace DQQ.Profiles.Affixes.Prefixes.Damages.PhysicsDamages
{
	[Pooled]
	public class PhysicsDamageProfileT03 : PhysicsDamageProfile
	{
		public override int AffixeLevel => 30;
		public override int TierLevel => 3;

		public override string? Name => "野蛮的";

		protected override EnumAffixeNumber number => EnumAffixeNumber.PhysicsDamageProfileT03;

		protected override int min => 21;
		protected override int max => 31;
	}
}
