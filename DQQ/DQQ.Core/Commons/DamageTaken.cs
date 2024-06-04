using DQQ.Components.Items;
using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Commons
{
  public class DamageTaken
  {
    public static DamageTaken New(DamageDeal[] damageDeals, bool killed)
    {
      return new DamageTaken
      {
        HitCheck = EnumHitCheck.Hit,
        DamagePoint = damageDeals.Sum(b => b.DamagePoint + b.CriticalDamagePoint),
        IsKilled = killed,
        Deals = damageDeals.Select(b => b).ToArray(),
      };
    }
    public static DamageTaken New(EnumHitCheck hit, bool killed)
    {
      return new DamageTaken
      {
        HitCheck = hit,
        DamagePoint = 0,
        IsKilled = killed,
        Deals = [],
      };
    }
    public EnumHitCheck HitCheck { get; set; }
    public Int64 DamagePoint { get; set; }
    public bool IsKilled { get; set; }
    public DamageDeal[]? Deals { get; set; }

    public IEnumerable<ItemComponent>? Drops { get; set; }
    public Int64 XP { get; set; }
    public bool DamageTakenSuccess { get; set; }
  }
}
