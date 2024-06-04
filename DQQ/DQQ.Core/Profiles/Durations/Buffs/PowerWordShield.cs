using DQQ.Attributes;
using DQQ.Components.Durations;
using DQQ.Components.Parameters;
using DQQ.Components.Stages;
using DQQ.Components.Stages.Maps;
using DQQ.Enums;
using DQQ.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Durations.Buffs
{
  [Pooled]
  public class PowerWordShield : DurationProfile
  {
    public override EnumDurationType? DurationType => EnumDurationType.Buff;

    public override EnumDurationNumber ProfileNumber => EnumDurationNumber.PowerWordShield;

    public override int StackLimitation => 1;
    public override bool ExtendIfFull => false;

    public override string? Name => "回复 (buff)";

    public override string? Discription => "周期性的回复生命, 持续5秒.";


    public override void BeforeDamageReduction(BeforeDamageTakenParameter parameter, DurationComponent component)
    {
      if (parameter.Damages?.Any() != true)
      {
        return;
      }
      foreach (var damage in parameter.Damages!.ToArray())
      {
        if (component.Power >= damage.DamagePoint)
        {
          component.Power = component.Power - damage.DamagePoint;
          damage.DamagePoint = 0;
          if (component.Power == 0)
          {
            component.TickRemain = 0;
          }
        }
        else
        {
          damage.DamagePoint = damage.DamagePoint - component.Power;
          damage.DamagePoint = 0;
          component.TickRemain = 0;
        }
      }
      base.BeforeDamageReduction(parameter, component);
    }
  }
}
