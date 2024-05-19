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

namespace DQQ.Components.Durations
{
  public class DurationComponent : DQQComponent, IDisposable
  {
    public EnumDurationNumber DurationNumber { get; set; }
    public DurationProfile? Duration => DQQPool.DurationPool[DurationNumber];
    public ITarget? Creator { get; set; }
    public int TickRemain { get; set; }
    public int TickCount = 0;
    public long TickPower = 0;
    private ITarget? lastTarget { get; set; }
    private IMap? lastMap { get; set; }

    public async Task<ContentResponse<bool>> OnTick(ITarget? target, IMap? map)
    {
      var result = new ContentResponse<bool>();
      TickRemain--;
      TickCount++;
      if (TickCount < DQQGeneral.DurationIntervalTick)
      {
        return result;
      }
      TickCount = 0;
      Duration?.OnActive(this, target, map);
      lastTarget = target;
      lastMap = map;
      return result;
    }
    bool isDisposed { get; set; }
    public void Dispose()
    {
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
