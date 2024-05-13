using DQQ.Components.Items;
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
    public static DamageTaken New(Int64 dt, bool killed)
    {
      return new DamageTaken
      {
        DamagePoint = dt,
        IsKilled = killed,
      };
    }
    public Int64 DamagePoint { get; set; }
    public bool IsKilled { get; set; }

    public IEnumerable<ItemComponent>? Drops { get; set; }
    public Int64 XP { get; set; }
    public bool DamageTakenSuccess { get; set; }
  }
}
