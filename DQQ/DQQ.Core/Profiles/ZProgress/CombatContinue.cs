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
	public class CombatContinue : ProgressProfile
	{
		public override EnumProgress ProfileNumber => EnumProgress.CombatContinue;
		public override string? Name => "自动战斗";
		public override string? Discription => "自动战斗";
		public override bool AvaliableCheck(Character? character)
		{
			return EnumChapter.C_1_4.IsUnlocked(character);
		}
	}
}
