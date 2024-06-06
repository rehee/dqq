using DQQ.Enums;

namespace DQQ.Profiles.Affixes.Prefixes.Damages.ColdDamages
{
	public abstract class ColdDamageProfile : DamageProfile
	{
		protected override EnumPropertyType propertyType => EnumPropertyType.ColdDamageModifier;
		protected override string discription => "冰冷";
	}
}
