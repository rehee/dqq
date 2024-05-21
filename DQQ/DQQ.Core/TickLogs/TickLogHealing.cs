using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.TickLogs
{
  public class TickLogHealing
  {
    public TickLogHealing()
    {

    }
    public TickLogHealing(long? healingDone, long? overHeal = null)
    {
      OverHeal = overHeal;
      HealingDone = healingDone;
    }
    public long? HealingDone { get; set; }
    public long? OverHeal { get; set; }
  }
}
