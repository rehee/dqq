using DQQ.TickLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Commons.DTOs
{
  public class CombatResultDTO
  {
    public bool Success { get; set; }
    public long XP { get; set; }
    public decimal TotalCombatminutes { get; set; }
    public DateTime CombatTime
    {
      get
      {
        return (new DateTime()).AddMinutes((double)TotalCombatminutes);
      }
    }
    public int DropItemNumber { get; set; }
    public TickLogItem[]? Logs { get; set; }

    public long DPS
    {
      get
      {
        return (Logs?.Where(b => b.From != null && b.From?.MobNumber == null).Select(b =>
        {
          long.TryParse(b.Damage?.DamagePoint, out var dmg);
          return dmg;
        }).Sum() ?? 0) / (long)(Logs?.Where(b => b.ActionSecond > 0).OrderByDescending(b => b.ActionSecond).Select(b => b.ActionSecond).FirstOrDefault() ?? 1);
      }
    }
  }
}
