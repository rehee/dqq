using DQQ.Attributes;
using DQQ.Components.Durations;
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
  public class RenewBuff : DurationProfile
  {
    public override EnumDurationType? DurationType => EnumDurationType.Healing;

    public override EnumDurationNumber ProfileNumber => EnumDurationNumber.Renew;

    public override string? Name => "回复 (buff)";

    public override string? Discription => "周期性的回复生命, 持续5秒.";

    public override void Healing(DurationComponent compose, ITarget? target, IMap? map)
    {
      var casterHealHp = (long)(target!.CombatPanel!.DynamicPanel!.MaximunLife! * 0.025m);
      target.TakeHealing(compose.Creator, casterHealHp, map, this);
    }
  }
}
