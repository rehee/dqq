using DQQ.Components.Durations;
using DQQ.Consts;
using DQQ.Enums;
using DQQ.Helper;
using DQQ.Pools;
using DQQ.Profiles.Durations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.TickLogs
{
	public class TickLogDuration
	{
		public static TickLogDuration? New(DurationComponent? compose)
		{
			if (compose?.DurationNumber == null)
			{
				return null;
			}
			var result = new TickLogDuration();

			result.DurationNumber = compose.DurationNumber;
			result.RemainSeconds = compose.TickRemain.GetTickSeconds(0);
			return result;
		}
		public EnumDurationNumber DurationNumber { get; set; }
		public DurationProfile? Profile => DQQPool.TryGet<DurationProfile, EnumDurationNumber>(DurationNumber);
		public decimal RemainSeconds { get; set; }
	}
}
