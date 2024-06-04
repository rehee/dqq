using DQQ.Attributes;
using DQQ.Components.Stages.Maps;
using DQQ.Components.Stages;
using DQQ.Enums;
using ReheeCmf.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DQQ.Durations;
using DQQ.Helper;
using DQQ.Pools;
using DQQ.Profiles.Durations;
using DQQ.Commons;

namespace DQQ.Profiles.Skills.Buffs
{
  [Pooled]
  public class PowerWordShield : AbHealing
  {
    public override bool NoPlayerSkill => false;
    protected override bool SelfTarget => true;
    public override decimal CastTime => 0m;

    public override decimal CoolDown => 15m;

    public override decimal DamageRate => 3m;

    public override bool CastWithWeaponSpeed => false;

    public override EnumSkill ProfileNumber => EnumSkill.PowerWordShield;

    public override string? Name => "真言术 盾";

    public override string? Discription => "释放一个护盾暂时保护自身";

    
    protected override HealingDeal[] CalculateHealing(ITarget? caster, IMap? map)
    {
      return [HealingDeal.New(CalculateDamage(caster, map).Sum(b => b.DamagePoint), EnumHealingType.HealingOverTime)];
    }
    protected override void DealingHealing(ITarget? from, HealingDeal[] healings, IMap? map)
    {
      base.DealingHealing(from, healings, map);
      var power = healings.Where(b => b.HealingType == EnumHealingType.HealingOverTime).Sum(b => b.Points);
      var durationParameter = new DurationParameter
      {
        Creator = from,
        DurationSeconds = 30,
        Value = power,
      };
      DQQPool.TryGet<DurationProfile, EnumDurationNumber?>(EnumDurationNumber.PowerWordShield)?.CreateDuration(durationParameter, from, map);
    }
  }
}
