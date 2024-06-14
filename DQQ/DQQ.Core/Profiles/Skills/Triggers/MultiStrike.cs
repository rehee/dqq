using DQQ.Attributes;
using DQQ.Components.Parameters;
using DQQ.Enums;
using DQQ.Helper;
using DQQ.Pools;
using ReheeCmf.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Skills.Triggers
{
	[Pooled]
	public class MultiStrike : AbTriggerSkillProfile
	{
		public override EnumSkillCategory Category => EnumSkillCategory.Enhancement;
		public override decimal CastTime => 0;
		public override decimal CoolDown => 0;
		public override decimal DamageRate => 0;
		public override bool CastWithWeaponSpeed => false;
		public override EnumSkillNumber ProfileNumber => EnumSkillNumber.MultiStrike;

		public int TriggerPercentate => 25;
		public override string? Name => "多重释放";
		public override string? Discription => $"释放技能并造成伤害后有 [{TriggerPercentate}]% 的几率再次释放";

		public override async Task<ContentResponse<bool>> CastSkill(ComponentTickParameter? parameter)
		{
			parameter?.Map!.AddMapLogSpillCast(true, parameter?.From, parameter?.SelectedTarget, this);
			return await parameter!.Trigger!.SkillProfile!.CastSkill(parameter);
		}

		public override bool AfterDealingDamageCheck(ComponentTickParameter parameter)
		{
			var random = RandomHelper.GetRandomInt(parameter.Random, 0, 100);
			return random <= 25;
		}

		public override async Task<ContentResponse<bool>> ActionOnAfterDealingDamage(ComponentTickParameter parameter)
		{
			return await CastSkill(parameter);
		}

	}
}
