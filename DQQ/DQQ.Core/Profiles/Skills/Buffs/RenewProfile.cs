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
  public class RenewProfile : AbHealing
  {
    public override bool NoPlayerSkill => false;
    protected override bool SelfTarget => true;
    public override decimal CastTime => 0m;

    public override decimal CoolDown => 5m;

    public override decimal DamageRate => 3m;

    public override bool CastWithWeaponSpeed => false;

    public override EnumSkill ProfileNumber => EnumSkill.Renew;

    public override string? Name => "回复";

    public override string? Discription => "周期性的持续回复生命";

    
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
        DurationSeconds = 5,
        Value = power
      };
      DQQPool.TryGet<DurationProfile, EnumDurationNumber?>(EnumDurationNumber.Renew)?.CreateDuration(durationParameter, from, map);
    }
  }
}
