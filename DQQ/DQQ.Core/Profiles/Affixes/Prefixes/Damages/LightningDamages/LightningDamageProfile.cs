using DQQ.Enums;

namespace DQQ.Profiles.Affixes.Prefixes.Damages.LightningDamages
{
	public abstract class LightningDamageProfile : DamageProfile
	{
		protected override EnumPropertyType propertyType => EnumPropertyType.LightningDamageModifier;
		protected override string discription => "闪电";
	}
}
