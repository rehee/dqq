using DQQ.TickLogs;
using Microsoft.AspNetCore.Components;

namespace DQQ.Web.Pages.DQQs.Combats
{
  public class CombatLogPage : DQQPageBase
  {
    [Parameter]
    public IEnumerable<TickLogItem>? CombatLog { get; set; }
  }
}