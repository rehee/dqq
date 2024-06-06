using DQQ.Attributes;
using DQQ.Enums;

namespace DQQ.Profiles.Affixes.Prefixes.Damages.PhysicsDamages
{
	[Pooled]
	public class PhysicsDamageProfileT02 : PhysicsDamageProfile
	{
		public override int AffixeLevel => 20;
		public override int TierLevel => 2;

		public override string? Name => "凶猛的";

		protected override EnumAffixeNumber number => EnumAffixeNumber.PhysicsDamageProfileT02;

		protected override int min => 13;
		protected override int max => 20;
	}
}
