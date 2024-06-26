﻿using DQQ.Attributes;
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
using DQQ.Components.Stages.Actors.Characters;

namespace DQQ.Profiles.Skills.Buffs
{
	[Pooled]
	public class RenewProfile : AbHealing
	{
		public override EnumChapter? UnLockedChapter => EnumChapter.C_1_4;
		public override bool NoPlayerSkill => false;
		public override decimal CastTime => 0m;

		public override decimal CoolDown => 5m;

		public override decimal DamageRate => 3m;

		public override bool CastWithWeaponSpeed => false;

		public override EnumSkillNumber ProfileNumber => EnumSkillNumber.Renew;

		public override string? Name => "回复";

		public override string? Discription => $"获得一个增益效果 持续 {GetDurationSeconds()} 秒. 在持续时间内周期性回复生命";


		protected override HealingDeal[] CalculateHealing(ComponentTickParameter? parameter)
		{
			return [HealingDeal.New(CalculateDamage(parameter).Sum(b => b.DamagePoint), EnumHealingType.HealingOverTime)];
		}

		public override int GetDurationSeconds()
		{
			return 5;
		}
		public override long GetDurationPower(ComponentTickParameter? parameter = null)
		{
			return parameter?.Healings?.Where(b => b.HealingType == EnumHealingType.HealingOverTime).Sum(b => b.Points) ?? 0;
		}
		protected override void DealingHealing(ComponentTickParameter? parameter)
		{
			base.DealingHealing(parameter);
			EnumDurationNumber.Renew.CreateDuration(parameter, this);
		}
	}
}
