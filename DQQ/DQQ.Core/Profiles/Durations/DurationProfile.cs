using DQQ.Components.Durations;
using DQQ.Components.Stages.Maps;
using DQQ.Components.Stages;
using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DQQ.Durations;
using DQQ.Consts;
using DQQ.Combats;

namespace DQQ.Profiles.Durations
{
  public abstract class DurationProfile : DQQProfile<EnumDurationNumber>
  {
    public abstract EnumDurationType? DurationType { get; }
    public virtual DurationComponent CreateDuration(DurationParameter? parameter, ITarget? target, IMap? map)
    {
      var result = new DurationComponent();
      result.DurationNumber = this.ProfileNumber;
      result.TickRemain = (int)((parameter?.DurationSeconds ?? 0) * DQQGeneral.TickPerSecond);
      var ticks = result.TickRemain == 0 ? 1 : (result.TickRemain / DQQGeneral.DurationIntervalTick);
      result.TickPower = ((parameter?.Value ?? 0) / ticks);
      result.TickPower = result.TickPower == 0 ? 1 : result.TickPower;
      result.Creator = parameter?.Creator;
      target?.Durations?.Add(result);
      return result;
    }

    public virtual void CombatPropertyCalculate(ICombatProperty combatProperty, ICombatProperty staticProperty, IMap map)
    {

    }

    public virtual void Healing(DurationComponent compose, ITarget? target, IMap? map)
    {

    }

    public virtual void Damage(DurationComponent compose, ITarget? target, IMap? map)
    {

    }

    public virtual void OnActive(DurationComponent compose, ITarget? target, IMap? map)
    {
      Healing(compose, target, map);
      Damage(compose, target, map);
    }

    public virtual void WhenGet()
    {

    }
    public virtual void WhenExpired()
    {

    }
  }
}
