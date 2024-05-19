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

namespace DQQ.Profiles.Durations.Debuffs
{
  [Pooled]
  public class RendDebuff : DurationProfile
  {
    public override EnumDurationType? DurationType => EnumDurationType.None;

    public override EnumDurationNumber ProfileNumber => EnumDurationNumber.Rend;

    public override string? Name => "撕裂 (debuff)";

    public override string? Discription => "周期性造成伤害";

    public override void Damage(DurationComponent compose, ITarget? target, IMap? map)
    {
      if (target == null)
      {
        return;
      }
      var result = target!.TakeDamage(compose.Creator, compose.TickPower, map);
      map!.AddMapLog(true, compose.Creator, target, this, result);
    }
  }
}
