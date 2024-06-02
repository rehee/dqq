using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Commons
{
  public class HealingDeal
  {
    public static HealingDeal New(long? point, EnumHealingType type = EnumHealingType.DirectHeal)
    {
      return new HealingDeal
      {
        Points = point ?? 0,
        HealingType = type,
      };
    }
    public long Points { get; set; }
    public EnumHealingType HealingType { get; set; }
  }
}
