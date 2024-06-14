using DQQ.Commons;
using DQQ.Components.Parameters;
using DQQ.Components.Stages;
using DQQ.Components.Stages.Maps;
using DQQ.Consts;
using DQQ.Enums;
using DQQ.Profiles;
using DQQ.Profiles.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Helper
{
	public static class HitCheckHelper
	{
		public static EnumHitCheck HitCheck(ComponentTickParameter parameter, SkillHitCheck? skillCheck)
		{
			if (skillCheck?.HitCheck == EnumHitCheck.Hit)
			{
				return EnumHitCheck.Hit;
			}
			if (parameter.To == null)
			{
				return EnumHitCheck.Hit;
			}
			if (MissCheck(parameter, skillCheck))
			{
				return EnumHitCheck.Miss;
			}

			if (DodgeCheck(parameter, skillCheck))
			{
				return EnumHitCheck.Dodge;
			}
			if (BlockCheck(parameter, skillCheck))
			{
				return EnumHitCheck.Block;
			}
			return EnumHitCheck.Hit;
		}

		public static bool BlockCheck(ComponentTickParameter parameter, SkillHitCheck? skillCheck)
		{
			if (skillCheck?.IgnoreCheck?.Any(b => b == EnumHitCheck.Block) == true)
			{
				return false;
			}
			var blockChance = (parameter?.To?.CombatPanel.DynamicPanel.BlockChance).DefaultValue();
			if (blockChance <= 0)
			{
				return false;
			}
			if (blockChance < RandomHelper.GetRandom(parameter.Random, 0))
			{
				return false;
			}
			return parameter?.To?.TryBlock().Success == true;
		}
		public static bool DodgeCheck(ComponentTickParameter parameter, SkillHitCheck? skillCheck)
		{
			if (skillCheck?.IgnoreCheck?.Any(b => b == EnumHitCheck.Dodge) == true)
			{
				return false;
			}
			var dogeChance = (parameter?.To?.CombatPanel.DynamicPanel.DodgeChance).DefaultValue();
			if (dogeChance <= 0)
			{
				return false;
			}

			return dogeChance >= RandomHelper.GetRandom(parameter.Random, 0);
		}
		public static bool MissCheck(ComponentTickParameter parameter, SkillHitCheck? skillCheck)
		{
			var hit = (parameter?.From?.Level).DefaultValue(1) + (parameter?.From?.CombatPanel?.DynamicPanel?.AttackRating).DefaultValue();
			var defence = (long)(((parameter?.To?.Level).DefaultValue(1) + (parameter?.To?.CombatPanel?.DynamicPanel?.Defence).DefaultValue())
				* (1 + (parameter?.To?.CombatPanel?.DynamicPanel?.DefencePercentage).DefaultValue()));
			var basicHitRate = DQQGeneral.CheckHitRate(hit, defence);
			var random = RandomHelper.GetRandom(parameter.Random, 0);
			return basicHitRate < random;
		}
	}
}
