using DQQ.Attributes;
using DQQ.Components.Stages.Actors.Characters;
using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.ZProgress
{
	[Pooled]
	public class MainTargetPriority : ProgressProfile
	{
		public override EnumProgress ProfileNumber => EnumProgress.MainTargetPriority;

		public override string? Name => "主目标优先级";

		public override string? Discription => "主目标优先级";

		public override bool AvaliableCheck(Character? character)
		{
			return character?.Level > 1;
		}
	}
}
