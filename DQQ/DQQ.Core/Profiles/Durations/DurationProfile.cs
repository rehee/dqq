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
using ReheeCmf.Responses;
using ReheeCmf.Helpers;
using DQQ.Components.Parameters;

namespace DQQ.Profiles.Durations
{
  public abstract class DurationProfile : DQQProfile<EnumDurationNumber>
  {
    public abstract EnumDurationType? DurationType { get; }
    public virtual int StackLimitation => 0;
    public virtual bool ExtendIfFull => true;
    public virtual ContentResponse<DurationComponent> CreateDuration(DurationParameter? parameter, ITarget? target, IMap? map)
    {
      var result = new ContentResponse<DurationComponent>();
      var sameDurations = target?.Durations?.Where(b => b.DurationNumber == this.ProfileNumber).ToArray();
      if (sameDurations?.Any() == true)
      {
        var count = sameDurations.Length;
        if (StackLimitation > 0)
        {
          if (StackLimitation <= count)
          {
            if (ExtendIfFull)
            {
              sameDurations.OrderBy(b => b.TickRemain).FirstOrDefault()!.TickRemain = (int)((parameter?.DurationSeconds ?? 0) * DQQGeneral.TickPerSecond);
            }
            return result;
          }

        }
      }
      result.SetSuccess(new DurationComponent() { });
      result.Content!.DurationNumber = this.ProfileNumber;
      result.Content!.TickRemain = (int)((parameter?.DurationSeconds ?? 0) * DQQGeneral.TickPerSecond);
      var ticks = result.Content!.TickRemain == 0 ? 1 : (result.Content!.TickRemain / DQQGeneral.DurationIntervalTick);
      if (DurationType == EnumDurationType.Buff)
      {
        result.Content!.Power = (parameter?.Value).GetValueOrDefault();
      }
      else
      {
        if (parameter?.Value != 0)
        {
          result.Content!.TickPower = ((parameter?.Value ?? 0) / ticks);
          result.Content!.TickPower = result.Content!.TickPower == 0 ? 0 : result.Content!.TickPower;

        }
      }

      result.Content!.Creator = parameter?.Creator;
      target?.Durations?.Add(result.Content!);
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

    public virtual void BeforeDamageReduction(BeforeDamageTakenParameter parameter, DurationComponent component)
    {

    }
    public virtual void DamageReduction(BeforeDamageTakenParameter parameter, DurationComponent component)
    {

    }
  }
}
