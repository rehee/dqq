using DQQ.Attributes;
using DQQ.Enums;

namespace DQQ.Profiles.Affixes.Prefixes.Damages.PhysicsDamages
{
	[Pooled]
	public class PhysicsDamageProfileT01 : PhysicsDamageProfile
	{
		public override int AffixeLevel => 10;
		public override int TierLevel => 1;

		public override string? Name => "强大的";

		protected override EnumAffixeNumber number => EnumAffixeNumber.PhysicsDamageProfileT01;

		protected override int min => 6;
		protected override int max => 12;
	}
}
