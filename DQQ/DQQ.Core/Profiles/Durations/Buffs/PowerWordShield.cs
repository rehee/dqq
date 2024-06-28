using DQQ.Attributes;
using DQQ.Components.Durations;
using DQQ.Components.Parameters;
using DQQ.Components.Stages;
using DQQ.Components.Stages.Maps;
using DQQ.Enums;
using DQQ.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Durations.Buffs
{
	[Pooled]
	public class PowerWordShield : DurationProfile
	{
		public override EnumDurationType? DurationType => EnumDurationType.Buff;

		public override EnumDurationNumber ProfileNumber => EnumDurationNumber.PowerWordShield;

		public override int StackLimitation => 1;
		public override bool ExtendIfFull => false;

		public override string? Name => "盾 (buff)";

		public override string? Discription => "吸收伤害";




		public override void BeforeDamageTaken(ComponentTickParameter parameter, DurationComponent component)
		{
			var damage = parameter.Damage?.DamagePoint ?? 0;
			if (damage <= 0)
			{
				return;
			}
			if (component.Power >= damage)
			{
				component.Power = component.Power - damage;
				parameter.Damage.DamagePoint = 0;
				parameter.Map?.AddMapLogHealingTaken(true, parameter?.From, parameter?.To, this, new TickLogs.TickLogHealing { Absorbe = damage });
			}
			else
			{
				parameter.Damage.DamagePoint = parameter.Damage.DamagePoint - component.Power;
				component.Power = 0;
				component.TickRemain = 0;
				parameter.Map?.AddMapLogHealingTaken(true, parameter?.From, parameter?.To, this, new TickLogs.TickLogHealing { Absorbe = component.Power });
			}
			if (component.Power == 0)
			{
				component.TickRemain = 0;
			}
		}
	}
}
