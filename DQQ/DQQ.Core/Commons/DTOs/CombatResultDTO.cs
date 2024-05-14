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
    public bool Success {  get; set; }
    public long XP {  get; set; } 
    public TickLogItem[]? Logs { get; set; }
  }
}
