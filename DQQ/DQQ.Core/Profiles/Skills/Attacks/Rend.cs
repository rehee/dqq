using DQQ.Attributes;
using DQQ.Commons;
using DQQ.Components.Parameters;
using DQQ.Components.Stages;
using DQQ.Components.Stages.Actors.Characters;
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

		public override EnumChapter? UnLockedChapter => EnumChapter.C_1_7;
		public override EnumSkillCategory Category => EnumSkillCategory.Core;
		public override EnumDamageHand DamageHand => EnumDamageHand.MainHand;
		public override decimal CastTime => 0;
		public override decimal CoolDown => 2.5m;
		public override decimal DamageRate => 6m;

		public override bool CastWithWeaponSpeed => false;

		public override EnumSkillNumber ProfileNumber => EnumSkillNumber.Rend;

		public override string? Name => "撕裂";

		public override string? Discription => $"使用武器撕裂敌人. 在 {GetDurationSeconds()} 秒 内造成主手武器伤害 600% 的流血伤害";

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
