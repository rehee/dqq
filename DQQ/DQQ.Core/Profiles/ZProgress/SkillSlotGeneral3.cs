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
	public class SkillSlotGeneral3 : ProgressProfile
	{
		public override EnumProgress ProfileNumber => EnumProgress.SkillSlotGeneral3;

		public override string? Name => "技能栏";

		public override string? Discription => "技能栏";

		public override bool AvaliableCheck(Character? character)
		{
			return EnumChapter.C_1_7.IsUnlocked(character);
		}
	}
}
