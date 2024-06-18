﻿using DQQ.Attributes;
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
	public class SkillTypeSupport : ProgressProfile
	{
		public override EnumProgress ProfileNumber => EnumProgress.SkillTypeSupport;

		public override string? Name => "辅助技能";

		public override string? Discription => "辅助技能";

		public override bool AvaliableCheck(Character? character)
		{
			return character?.Level > 5;
		}
	}
}
