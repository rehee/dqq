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
    public static BigInteger GetNextLevelXP(int targetLevel, BigInteger? xp = null, int? level = null)
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
      return xp?? XPConst.Level1XP;
    }
  }
}
