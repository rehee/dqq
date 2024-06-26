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
        if(HitCheck == EnumHitCheck.Miss)
        {
          var a = 1;
        }
        switch (HitCheck)
        {
          case EnumHitCheck.Hit: return DamagePoint ?? "0";
          case EnumHitCheck.Miss: return "Miss";
          case EnumHitCheck.Block: return "Block";
          case EnumHitCheck.Dodge: return "Dodge";

        }
        return "";
      }
    }
  }
}
