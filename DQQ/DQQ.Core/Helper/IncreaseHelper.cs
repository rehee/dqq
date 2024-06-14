using DQQ.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Helper
{
	public static class IncreaseHelper
	{
		public static long WeponDamageIncrease(this int itemLevel, Random? random, decimal damageRage = 1)
		{
			if (itemLevel < 1)
			{
				itemLevel = 1;
			}
			if (itemLevel >= DQQGeneral.MaxLevel)
			{
				itemLevel = DQQGeneral.MaxLevel;
			}
			double dps = DQQGeneral.InitialWeaponDPS + (DQQGeneral.MaxWeaponDPS - DQQGeneral.InitialWeaponDPS) * (itemLevel - 1) / (DQQGeneral.MaxLevel - 1);
			var actual = (long)Math.Round(dps, 0);
			if (random != null)
			{
				return actual.GetRandomRange(random, DQQGeneral.WeaponDamageRange);
			}
			return actual;
		}
	}
}
