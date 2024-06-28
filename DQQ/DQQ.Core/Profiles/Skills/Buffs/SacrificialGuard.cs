using DQQ.Attributes;
using DQQ.Components.Parameters;
using DQQ.Enums;
using DQQ.Pools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Skills.Buffs
{
	[Pooled]
	public class SacrificialGuard: AbHealing
	{
		public override EnumTarget? TargetForce => EnumTarget.Friendly;

		public override bool NoPlayerSkill => true;
		public override decimal CastTime => 0;
		public override decimal CoolDown => 15;
		public override decimal DamageRate => 0;
		public override bool CastWithWeaponSpeed => false;
		public override EnumSkillNumber ProfileNumber => EnumSkillNumber.SacrificialGuard;
		public override string? Name => "牺牲守护";
		public override string? Discription => "牺牲守护";
		public override long GetDurationPower(ComponentTickParameter? parameter = null)
		{
			return base.GetDurationPower(parameter);
		}
		public override int GetDurationSeconds()
		{
			return 10;
		}
		protected override void DealingHealing(ComponentTickParameter? parameter)
		{
			var friends = parameter?.FriendlyTargets?.Where(b => b.Alive && b.DisplayId != parameter?.From?.DisplayId).ToArray();
			if (friends?.Any() != true)
			{
				return;
			}
			foreach (var friend in friends) 
			{
				var target = ComponentTickParameter.New(parameter, friend);
				
				EnumDurationNumber.SacrificialGuard.CreateDuration(target, this);
			}
			
			
		}
	}
}
