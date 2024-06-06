using DQQ.Attributes;
using DQQ.Enums;

namespace DQQ.Profiles.Affixes.Prefixes.Damages.PhysicsDamages
{
	[Pooled]
	public class PhysicsDamageProfileT0 : PhysicsDamageProfile
	{
		public override int AffixeLevel => 0;
		public override int TierLevel => 0;

		public override string? Name => "有力的";

		protected override EnumAffixeNumber number => EnumAffixeNumber.PhysicsDamageProfileT0;

		protected override int min => 1;
		protected override int max => 5;
	}
}
