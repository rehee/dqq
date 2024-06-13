using DQQ.Attributes;
using DQQ.Commons;
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

namespace DQQ.Profiles.Durations.Debuffs
{
  [Pooled]
  public class RendDebuff : DurationProfile
  {
    public override EnumDurationType? DurationType => EnumDurationType.Damage;

    public override EnumDurationNumber ProfileNumber => EnumDurationNumber.Rend;

    public override string? Name => "撕裂 (debuff)";

    public override string? Discription => "周期性造成伤害";

    public override void Damage(DurationComponent compose, ITarget? target, IMap? map)
    {
      if (target == null)
      {
        return;
      }
      var tick = compose.TickPower<=0 ? 1 : compose.TickPower;

			var result = target!.TakeDamage(ComponentTickParameter.New(compose.Creator, target, map, this, DamageDeal.New(tick)));

    }
  }
}
