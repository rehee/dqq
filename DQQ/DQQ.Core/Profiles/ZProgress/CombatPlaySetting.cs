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
	public class CombatPlaySetting : ProgressProfile
	{
		public override EnumProgress ProfileNumber => EnumProgress.CombatPlaySetting;

		public override string? Name => "演出设置";

		public override string? Discription => "演出设置";

		public override bool AvaliableCheck(Character? character)
		{
			return character?.Level > 15;
		}
	}
}
