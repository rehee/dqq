using DQQ.Attributes;
using DQQ.Commons;
using DQQ.Components.Parameters;
using DQQ.Components.Stages;
using DQQ.Components.Stages.Maps;
using DQQ.Durations;
using DQQ.Enums;
using DQQ.Helper;
using DQQ.Pools;
using DQQ.Profiles.Durations;
using DQQ.Profiles.Durations.Debuffs;
using ReheeCmf.Helpers;
using ReheeCmf.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Skills.Attacks
{
	[Pooled]
	public class Rend : GeneralSkill
	{
		public override EnumSkillCategory Category => EnumSkillCategory.Core;
		public override EnumDamageHand DamageHand => EnumDamageHand.MainHand;
		public override decimal CastTime => 0;
		public override decimal CoolDown => 1.5m;
		public override decimal DamageRate => 3m;

		public override bool CastWithWeaponSpeed => false;

		public override EnumSkillNumber ProfileNumber => EnumSkillNumber.Rend;

		public override string? Name => "撕裂";

		public override string? Discription => $"使用武器撕裂敌人. 在 {GetDurationSeconds()} 秒内造成住手武器伤害300%的流血伤害";

		public override long GetDurationPower(ComponentTickParameter? parameter = null)
		{
			var rendDamage = CalculateDamage(parameter);
			return rendDamage.Sum(b => b.DamagePoint);
		}
		public override int GetDurationSeconds()
		{
			return 5;
		}

		protected override async Task DealingDamage(ComponentTickParameter? parameter, DamageDeal[] damageDeals, IMap? map)
		{
			await base.DealingDamage(parameter, [], map);
		}
		protected override async Task AfterDealingDamage(ComponentTickParameter? parameter)
		{
			await base.AfterDealingDamage(parameter);
			EnumDurationNumber.Rend.CreateDuration(parameter, this);
		}

	}
}
