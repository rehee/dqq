using DQQ.Components.Items;
using DQQ.TickLogs;
using System.Numerics;

namespace DQQ.Commons
{
  public class TickResult
  {
    public bool Success { get; set; }
    public IEnumerable<TickLogItem>? Logs { get; set; }
    public IEnumerable<ItemComponent>? Dropping { get; set; }
    public Int64 XP { get; set; }
  }
}
