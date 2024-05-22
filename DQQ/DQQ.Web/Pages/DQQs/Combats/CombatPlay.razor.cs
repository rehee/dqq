using BootstrapBlazor.Components;
using DQQ.Components;
using DQQ.TickLogs;
using Microsoft.AspNetCore.Components;

namespace DQQ.Web.Pages.DQQs.Combats
{
  public class CombatPlayPage : DQQPageBase
  {
    [Parameter]
    public IEnumerable<TickLogItem>? CombatLog { get; set; }

    public int Tick { get; set; }
    public IEnumerable<TickLogActor>? Players { get; set; }
    public IEnumerable<TickLogActor>? Enemies { get; set; }

    private bool boolPageIsLoad { get; set; }

    public List<ConsoleMessageItem> MessageItems { get; set; } = new List<ConsoleMessageItem>();
    protected async override void OnAfterRender(bool firstRender)
    {
      base.OnAfterRender(firstRender);
      if (firstRender)
      {
        boolPageIsLoad = true;
      }
    }

    protected override async Task OnInitializedAsync()
    {
      await base.OnInitializedAsync();
      KeepRefresh();
    }

    public async Task KeepRefresh()
    {

      if (CombatLog?.Any() != true || boolPageIsLoad != true)
      {
        await Task.Delay(30);
        KeepRefresh();
        return;
      }
      var lastTick = CombatLog.OrderByDescending(b => b.ActionTick).Select(b => b.ActionTick).FirstOrDefault();
      if (lastTick == null)
      {
        return;
      }
      MessageItems.Clear();
      for (var i = 0; i <= lastTick; i++)
      {

        await Task.Delay(1000 / 30);

        var logs = CombatLog.Where(b => b.ActionTick == i).ToArray();
        foreach (var log in logs)
        {
          if (log.WaveNumber < 0)
          {
            continue;
          }
          Tick = log.WaveNumber;
          MessageItems.Add(log.GetConsoleMessage());
          if (log.LogType == Enums.EnumLogType.WaveChange)
          {

            Players = log.Players?.ToArray();
            Enemies = log.Enemies?.ToArray();
            StateHasChanged();
            await Task.Delay(1000);

          }
          else
          {
            Players = log.Players?.ToArray();
            Enemies = log.Enemies?.ToArray();
            StateHasChanged();
            if (Enemies?.All(b => b.PercentageHP <= 0) == true || Players?.All(b => b.PercentageHP <= 0) == true)
            {
              await Task.Delay(3000);
            }
            else
            {
              await Task.Delay(1000 / 30);
            }

          }


        }

      }
    }
  }
}