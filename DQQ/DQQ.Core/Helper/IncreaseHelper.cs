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
    public static double WeaponDamagePower = 2.523d;
    public static double GeneralWeaponDamage = 6;
    public static long WeponDamageIncrease(this int itemLevel, decimal damageRage = 1)
    {
      if (itemLevel < 1)
      {
        itemLevel = 1;
      }
      var baseDamage = (long)Math.Round(GeneralWeaponDamage * Math.Pow(itemLevel, WeaponDamagePower) * (double)damageRage, 0);
      return baseDamage.GetRandomRange(80);
    }
  }
}
