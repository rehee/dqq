using DQQ.Attributes;
using DQQ.Enums;

namespace DQQ.Profiles.Skills.Supports
{
	[Pooled]
	public class EmpowerSupport : AbSupportSkillProfile
	{
		public override decimal CastTime => 0;

		public override decimal CoolDown => 0;

		public override decimal DamageRate => 0;

		public override bool CastWithWeaponSpeed => false;

		public override EnumSkillNumber ProfileNumber => EnumSkillNumber.EmpowerSupport;

		public override string? Name => "强化";

		public override string? Discription => "强化";
	}
}
