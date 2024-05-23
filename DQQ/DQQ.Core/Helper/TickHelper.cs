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
    public static Decimal GetTickSeconds(this int tick, int digital = 2)
    {
      return Math.Round(tick / (decimal)DQQGeneral.TickPerSecond, digital);
    }
    public static Decimal GetTickSeconds(this int? tick, int digital = 2)
    {
      return GetTickSeconds((tick ?? -1), digital);
    }
  }
}
