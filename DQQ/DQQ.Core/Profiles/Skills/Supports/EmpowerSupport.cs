using DQQ.Attributes;
using DQQ.Commons;
using DQQ.Components.Parameters;
using DQQ.Enums;
using DQQ.Helper;

namespace DQQ.Profiles.Skills.Supports
{
	[Pooled]
	public class EmpowerSupport : AbSupportSkillProfile
	{
		public override decimal DamageRate => 0m;
		public override EnumSkillCategory Category => EnumSkillCategory.Enhancement;
		public override EnumSkillNumber ProfileNumber => EnumSkillNumber.EmpowerSupport;
		public int Increase => 15;
		public decimal IncreasePercentage => (100 + Increase) / 100m;
		public override string? Name => "强化";
		public override string? Discription => $"增强被辅助的技能伤害 {Increase}%";

		public override DamageDeal[] SupportDamageCalculate(DamageDeal[] damage, ComponentTickParameter? parameter)
		{
			foreach (var d in damage)
			{
				d.DamagePoint = d.DamagePoint.Percentage(IncreasePercentage);
			}
			return damage;
		}
	}
}
