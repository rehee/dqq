using DQQ.Consts;
using DQQ.XPs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Helper
{
	public static class XPHelper
	{
		public static Int64 GetNextLevelXP(int targetLevel, Int64? xp = null, int? level = null)
		{
			if (targetLevel <= 1)
			{
				return XPConst.Level1XP;
			}
			if (level == null)
			{
				return GetNextLevelXP(targetLevel, XPConst.Level1XP, 1);
			}

			if (level < targetLevel)
			{
				var percentage = XPConst.XPStages.Where(b => b.MinLevel <= level && b.MaxLevel > level).FirstOrDefault();
				var newXP = (xp ?? XPConst.Level1XP).Percentage(1 + (percentage?.Rate ?? 0));
				return GetNextLevelXP(targetLevel, newXP, level + 1);
			}
			return xp ?? XPConst.Level1XP;
		}

		public static BigInteger GetNextLevelUpExp(int currentLevel)
		{
			currentLevel = Math.Max(currentLevel, 1);
			currentLevel = Math.Min(currentLevel, 69);


			double baseExperience = DQQGeneral.BasicLevelUpExp;
			double multiplier = DQQGeneral.LevelUpExpIncreased;
			double experience = baseExperience * Math.Pow(multiplier, currentLevel - 1);
			return new BigInteger(Math.Round(experience));
		}
		public static ExperienceAndLevel CheckExperienceAndLevelUP(ExperienceAndLevel input)
		{
			var nextLevelXP = GetNextLevelUpExp(input.Level);
			if (nextLevelXP <= 0)
			{
				return input;
			}
			if (nextLevelXP > input.Experience)
			{
				return input;
			}
			input.Experience = input.Experience - nextLevelXP;
			input.Level = input.Level + 1;
			return CheckExperienceAndLevelUP(input);
		}

		public static long GetMobKilledExp(int level)
		{
			if (level <= 1)
			{
				level = 1;
			}
			return (long)Math.Round(DQQGeneral.BasicMobExp * Math.Pow(1 + DQQGeneral.LevelMobExpIncreased, level - 1), 0);
		}
	}

	public class ExperienceAndLevel
	{
		private ExperienceAndLevel()
		{

		}
		public static ExperienceAndLevel New(int? level, BigInteger? experience)
		{
			var lev = level.HasValue ? level <= 0 ? 1 : level.Value : 1;
			var exp = experience ?? 0;
			return new ExperienceAndLevel
			{
				Level = lev,
				Experience = exp,
			};
		}
		public int Level { get; set; }
		public BigInteger Experience { get; set; }
	}
}
