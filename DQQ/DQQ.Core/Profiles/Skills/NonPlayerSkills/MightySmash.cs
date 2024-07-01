using DQQ.Attributes;
using DQQ.Commons;
using DQQ.Components.Parameters;
using DQQ.Components.Stages;
using DQQ.Components.Stages.Maps;
using DQQ.Enums;
using ReheeCmf.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Skills.NonPlayerSkills
{
	[Pooled]
	public class MightySmash : NonPlayerSKill
	{
		public override decimal CastTime => 1.5m;

		public override decimal CoolDown => 15m;

		public override decimal DamageRate => 10m;

		public override SkillHitCheck? CheckHit(ComponentTickParameter parameter)
		{
			return new SkillHitCheck { HitCheck = EnumHitCheck.Hit };
		}
		public override bool CastWithWeaponSpeed => false;

		public override EnumSkillNumber ProfileNumber => EnumSkillNumber.MightySmash;

		public override string? Name => "巨力重击";
		public override string? Discription => "巨力重击";

		public override EnumTarget? TargetForce => EnumTarget.Target;

		public override bool AvaliableTarget(ComponentTickParameter? parameter)
		{
			if(parameter==null) return false;
			if(parameter.To==null)return false;
			if(parameter.To.PercentageHP>30) return false;
			return true;
		}
	}
}
