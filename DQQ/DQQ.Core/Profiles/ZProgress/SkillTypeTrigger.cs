using DQQ.Attributes;
using DQQ.Components.Stages.Actors.Characters;
using DQQ.Enums;
using DQQ.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.ZProgress
{
	[Pooled]
	public class SkillTypeTrigger : ProgressProfile
	{
		public override EnumProgress ProfileNumber => EnumProgress.SkillTypeTrigger;

		public override string? Name => "触发技能";

		public override string? Discription => "触发技能";

		public override bool AvaliableCheck(Character? character)
		{
			return character?.Level > 20;
		}
	}
}
