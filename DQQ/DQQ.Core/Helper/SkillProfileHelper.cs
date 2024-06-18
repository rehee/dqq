using DQQ.Components.Stages.Actors.Characters;
using DQQ.Enums;
using DQQ.Pools;
using DQQ.Profiles.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Helper
{
	public static class SkillProfileHelper
	{
		public static IEnumerable<SkillProfile> PlayerAvaliableSkill(this IEnumerable<SkillProfile> profiles, int? level = null)
		{
			return profiles.Where(b => b.IsPlayerAvaliableSkill(level));
		}

		public static bool IsPlayerAvaliableSkill(this SkillProfile profiles, int? level = null)
		{
			if (profiles.NoPlayerSkill)
			{
				return false;
			}
			if (level == null)
			{
				return true;
			}
			return level >= profiles.CharacterLevelRequired;
		}
		public static bool IsPlayerAvaliableSkill(this EnumSkillNumber profiles, int? level = null)
		{
			return DQQPool.TryGet<SkillProfile, EnumSkillNumber>(profiles)?.IsPlayerAvaliableSkill(level) == true;
		}
	}
}
