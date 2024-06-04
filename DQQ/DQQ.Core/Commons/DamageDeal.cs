using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Commons
{
  public class DamageDeal
  {
    public static DamageDeal New(long damage, EnumDamageType damageType = EnumDamageType.Physics)
    {
      return new DamageDeal
      {
        DamagePoint = damage,
        DamageType = damageType,
      };
    }
    public long DamagePoint { get; set; }
    public EnumDamageType DamageType { get; set; }
    public bool IsCritical { get; set; }
    public long CriticalDamagePoint { get; set; }
  }
}
