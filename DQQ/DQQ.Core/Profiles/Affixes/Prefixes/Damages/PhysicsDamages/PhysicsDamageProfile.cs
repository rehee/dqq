using DQQ.Enums;

namespace DQQ.Profiles.Affixes.Prefixes.Damages.PhysicsDamages
{
	public abstract class PhysicsDamageProfile : DamageProfile
	{
		protected override EnumPropertyType propertyType => EnumPropertyType.PhysicsDamageModifier;
		protected override string discription => "物理";
	}
}
