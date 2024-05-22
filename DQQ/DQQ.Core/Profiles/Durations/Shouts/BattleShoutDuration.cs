using DQQ.Attributes;
using DQQ.Combats;
using DQQ.Components.Stages.Maps;
using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Durations.Shouts
{
  [Pooled]
  public class BattleShoutDuration : DurationProfile
  {
    public override EnumDurationType? DurationType => EnumDurationType.Buff;

    public override EnumDurationNumber ProfileNumber => EnumDurationNumber.BattleShout;

    public override string? Name => "战吼 (buff)";

    public override string? Discription => "攻击力增加";

    public override void CombatPropertyCalculate(ICombatProperty combatProperty, ICombatProperty staticProperty, IMap map)
    {
      combatProperty.DamageModifier = (combatProperty.DamageModifier ?? 0) + 0.1m;
    }
  }
}
