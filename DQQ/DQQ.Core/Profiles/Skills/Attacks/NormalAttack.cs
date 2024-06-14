using DQQ.Attributes;
using DQQ.Components.Parameters;
using DQQ.Components.Stages;
using DQQ.Components.Stages.Actors;
using DQQ.Components.Stages.Actors.Characters;
using DQQ.Components.Stages.Maps;
using DQQ.Enums;
using DQQ.Helper;
using ReheeCmf.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Skills.Attacks
{
	[Pooled]
	public class NormalAttack : GeneralSkill
	{

		public override EnumDamageHand DamageHand => EnumDamageHand.EachHand;
		public override decimal CastTime => 1.2m;
		public override decimal CoolDown => 0m;
		public override decimal DamageRate => 1m;
		public override string? Discription => "使用武器进行普通攻击. 攻击速度基于武器速度. 双持的时候攻击速度增加,主副手轮流攻击";
		public override bool CastWithWeaponSpeed => true;
		public override EnumSkillNumber ProfileNumber => EnumSkillNumber.NormalAttack;
		public override string? Name => "普通攻击";

		public override EnumSkillTag[]? OriginalTag => [EnumSkillTag.Attack];

		public override Task<ContentResponse<bool>> CastSkill(ComponentTickParameter? parameter)
		{
			return base.CastSkill(parameter);
		}
	}
}