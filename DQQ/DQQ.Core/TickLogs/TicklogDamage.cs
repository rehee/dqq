using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.TickLogs
{
  public class TicklogDamage
  {
    public string? DamagePoint { get; set; }
    public bool IsKilled { get; set; }
    public EnumHitCheck? HitCheck { get; set; }

    public string? DisplayDamage
    {
      get
      {
        switch (HitCheck)
        {
          case EnumHitCheck.Hit: return DamagePoint ?? "";
          case EnumHitCheck.Miss: return "Miss";
          case EnumHitCheck.Block: return "Block";
          case EnumHitCheck.Dodge: return "Dodge";

        }
        return "";
      }
    }
  }
}
