using DQQ.Enums;

namespace DQQ.Profiles.Affixes.Prefixes.Damages.FireDamages
{
	public abstract class FireDamageProfile : DamageProfile
	{
		protected override EnumPropertyType propertyType => EnumPropertyType.FireDamageModifier;
		protected override string discription => "火焰";
	}
}
