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
	public class SkillSupport2 : ProgressProfile
	{
		public override EnumProgress ProfileNumber => EnumProgress.SkillSupport2;

		public override string? Name => "辅助技能";

		public override string? Discription => "辅助技能";

		public override bool AvaliableCheck(Character? character)
		{
			return character?.Level > 10;
		}
	}
}
