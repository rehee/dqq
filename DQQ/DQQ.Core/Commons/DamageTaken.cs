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
    public static DamageTaken New(BigInteger dt, bool killed)
    {
      return new DamageTaken
      {
        DamagePoint = dt,
        IsKilled = killed,
      };
    }
    public BigInteger DamagePoint { get; set; }
    public bool IsKilled { get; set; }

    public IEnumerable<ItemComponent>? Drops { get; set; }
    public BigInteger XP { get; set; }
    public bool DamageTakenSuccess { get; set; }
  }
}
