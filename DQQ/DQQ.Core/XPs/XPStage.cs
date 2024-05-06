using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.XPs
{
  public class XPStage
  {

    public XPStage(int minLevel, int maxLevel, decimal rate)
    {
      MinLevel = minLevel;
      MaxLevel = maxLevel;
      Rate = rate;
    }
    public int MinLevel { get; set; }
    public int MaxLevel { get; set; }
    public decimal Rate { get; set; }
  }
}
