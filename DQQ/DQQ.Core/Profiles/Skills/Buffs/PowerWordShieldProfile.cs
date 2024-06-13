using DQQ.Attributes;
using DQQ.Components.Stages.Maps;
using DQQ.Components.Stages;
using DQQ.Enums;
using ReheeCmf.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DQQ.Durations;
using DQQ.Helper;
using DQQ.Pools;
using DQQ.Profiles.Durations;
using DQQ.Commons;
using DQQ.Components.Parameters;

namespace DQQ.Profiles.Skills.Buffs
{
	[Pooled]
	public class PowerWordShield : AbHealing
	{
		public override bool NoPlayerSkill => false;
		public override bool SelfTarget => true;
		public override decimal CastTime => 0m;

		public override decimal CoolDown => 15m;

		public override decimal DamageRate => 3m;

		public override bool CastWithWeaponSpeed => false;

		public override EnumSkillNumber ProfileNumber => EnumSkillNumber.PowerWordShield;

		public override string? Name => "真言术 盾";

		public override string? Discription => $"释放一个护盾暂时保护自身 吸收一定的伤害 持续 {GetDurationSeconds()} 秒";


		protected override HealingDeal[] CalculateHealing(ComponentTickParameter? parameter)
		{
			return [HealingDeal.New(CalculateDamage(parameter).Sum(b => b.DamagePoint), EnumHealingType.HealingOverTime)];
		}
		public override int GetDurationSeconds()
		{
			return 30;
		}
		public override long GetDurationPower(ComponentTickParameter? parameter = null)
		{
			return parameter?.Healings?.Where(b => b.HealingType == EnumHealingType.HealingOverTime).Sum(b => b.Points) ?? 0;
		}

		protected override void DealingHealing(ComponentTickParameter? parameter)
		{
			base.DealingHealing(parameter);
			EnumDurationNumber.PowerWordShield.CreateDuration(parameter, this);
		}
	}
}
