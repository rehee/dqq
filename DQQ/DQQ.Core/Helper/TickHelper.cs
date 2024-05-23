using DQQ.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Helper
{
  public static class TickHelper
  {
    public static Decimal GetTickSeconds(this int tick)
    {
      return Math.Round(tick / (decimal)DQQGeneral.TickPerSecond, 2);
    }
    public static Decimal GetTickSeconds(this int? tick)
    {
      return GetTickSeconds(tick ?? -1);
    }
  }
}
