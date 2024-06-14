using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Consts
{
	public static class MonsterBasicProfile
	{
		public const double BaseHP = 50;
		public const double BasicDPS = 5;

		public const decimal AttackPerSecond = 1;
		public const double MonsterIncreaseRate = 1;

		public static double CalculateValue(int? level, double baseValue, double increasePercentage)
		{
			if (level == null || level <= 0)
			{
				level = 1;
			}
			double value = baseValue * Math.Pow(1 + increasePercentage, level.Value - 1);
			return value;
		}
	}
}
