using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.XPs
{
  public static class XPConst
  {
    public static Int64 Level1XP => 100;
    public static IEnumerable<XPStage> XPStages => new XPStage[]
    {
      new XPStage(1,20,0.10m),
      new XPStage(21,40,0.15m),
      new XPStage(41,100,0.20m),
      new XPStage(101,300,0.25m),
      new XPStage(301,0,0.3m),
    };
  }
}
