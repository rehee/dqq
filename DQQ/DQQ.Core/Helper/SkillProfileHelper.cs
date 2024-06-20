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
		public static IEnumerable<SkillProfile> PlayerAvaliableSkill(this IEnumerable<SkillProfile> profiles, Character? character = null)
		{
			return profiles.Where(b => b.IsPlayerAvaliableSkill(character));
		}

		public static bool IsPlayerAvaliableSkill(this SkillProfile profiles, Character? character = null,bool onlyCheckPlayer=false)
		{
			if (profiles.NoPlayerSkill)
			{
				return false;
			}
			if (onlyCheckPlayer)
			{
				return true;
			}
			return profiles.IsAvaliableForCharacter(character);
		}
		public static bool IsPlayerAvaliableSkill(this EnumSkillNumber profiles, Character? character = null)
		{
			return DQQPool.TryGet<SkillProfile, EnumSkillNumber>(profiles)?.IsPlayerAvaliableSkill(character) == true;
		}
	}
}
