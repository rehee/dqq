using DQQ.Components.Stages.Maps;
using DQQ.Components.Stages;
using ReheeCmf.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DQQ.Enums;
using DQQ.Profiles.Durations;
using DQQ.Consts;
using DQQ.Pools;
using DQQ.Combats;
using DQQ.Profiles.Skills;
using DQQ.Components.Parameters;

namespace DQQ.Components.Durations
{
  public class DurationComponent : DQQComponent, IDisposable
  {
    public EnumDurationNumber DurationNumber { get; set; }
    public DurationProfile? Duration => DQQPool.TryGet<DurationProfile, EnumDurationNumber?>(DurationNumber);
    public ITarget? Creator { get; set; }
    public int TickRemain { get; set; }
    public int TickCount = 0;
    public long TickPower = 0;
    private ITarget? lastTarget { get; set; }
    private IMap? lastMap { get; set; }

    public void CombatPropertyCalculate(ICombatProperty combatProperty, ICombatProperty staticProperty, IMap map)
    {
      if (Duration != null)
      {
        Duration.CombatPropertyCalculate(combatProperty, staticProperty, map);
      }
    }

    public override async Task<ContentResponse<bool>> OnTick(ComponentTickParameter parameter)
    {
      var result = await base.OnTick(parameter);
      if (!result.Success)
      {
        return result;
      }
      TickRemain--;
      TickCount++;
      if (TickCount < DQQGeneral.DurationIntervalTick)
      {
        return result;
      }
      TickCount = 0;
      Duration?.OnActive(this, parameter.From, parameter.Map);
      lastTarget = parameter.From;
      lastMap = parameter.Map;
      return result;
    }

    bool isDisposed { get; set; }
    public override void Dispose()
    {
      base.Dispose();
      if (isDisposed)
      {
        return;
      }
      isDisposed = true;
      if (TickCount > 0)
      {
        Duration?.OnActive(this, lastTarget, lastMap);
      }
      Duration?.WhenExpired();
      lastMap = null;
      lastTarget = null;
      Creator = null;
    }

  }
}
