using DQQ.Attributes;
using DQQ.Commons;
using DQQ.Components.Durations;
using DQQ.Components.Parameters;
using DQQ.Components.Stages;
using DQQ.Components.Stages.Maps;
using DQQ.Enums;
using DQQ.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Durations.Buffs
{
	[Pooled]
	public class SacrificialGuard : DurationProfile
	{
		public override EnumDurationType? DurationType => EnumDurationType.Buff;

		public override EnumDurationNumber ProfileNumber => EnumDurationNumber.SacrificialGuard;

		public override string? Name => "牺牲守护 (buff)";

		public override string? Discription => "伤害转移";
		public decimal DamageTransfer => 0.99m;

		public override void BeforeDamageTaken(ComponentTickParameter parameter, DurationComponent component)
		{
			if (parameter.Damage?.DamagePoint == null || parameter.Damage?.DamageTakenSuccess!=true || component.Creator?.Alive!= true)
			{
				return;
			}
			var damage = parameter.Damage.DamagePoint * DamageTransfer;

			parameter.Damage.DamagePoint = parameter.Damage.DamagePoint - (long)damage;
			var secondParameter = ComponentTickParameter.New(parameter, this, DamageDeal.New((long)(damage/2)));
			secondParameter.SecondaryTarget = component.Creator;
			component.Creator?.TakeDamage(secondParameter);
		}
	}
}
