using DQQ.Attributes;
using DQQ.Commons;
using DQQ.Components.Parameters;
using DQQ.Components.Stages.Actors.Characters;
using DQQ.Enums;

namespace DQQ.Profiles.Skills.Buffs
{
	[Pooled]
	public class DarkGreaterHeal : AbHealing
	{
		
		public override bool NoPlayerSkill => true;
		
		public override decimal CastTime => 3m;

		public override decimal CoolDown => 0m;

		public override decimal DamageRate => 15m;

		public override bool CastWithWeaponSpeed => false;

		public override EnumSkillNumber ProfileNumber => EnumSkillNumber.DarkGreaterHeal;

		public override string? Name => "大黑暗治疗";

		public override string? Discription => "强效治疗治疗一个目标";

		protected override HealingDeal[] CalculateHealing(ComponentTickParameter? parameter)
		{
			var damage = CalculateDamage(parameter);
			return [HealingDeal.New(damage.Sum(b => b.DamagePoint))];
		}

	}
}
