using DQQ.Components.Parameters;
using DQQ.Durations;
using DQQ.Enums;
using DQQ.Pools;
using DQQ.Profiles.Durations;
using DQQ.Profiles.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Helper
{
	public static class DurationHelper
	{
		public static void CreateDuration(this EnumDurationNumber number, ComponentTickParameter? parameter, SkillProfile? profile)
		{
			var duration = DQQPool.TryGet<DurationProfile, EnumDurationNumber?>(number);
			if (duration == null)
			{
				return;
			}
			if (parameter == null)
			{
				return;
			}
			var durationParameter = new DurationParameter
			{
				Creator = parameter?.From,
				DurationSeconds = profile?.GetDurationSeconds() ?? 0,
				Value = profile?.GetDurationPower(parameter) ?? 0,
			};
			duration.CreateDuration(durationParameter, profile?.GetTarget(parameter), parameter?.Map);
		}
	}
}
