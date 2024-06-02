using DQQ.Attributes;
using DQQ.Commons;
using DQQ.Components.Stages;
using DQQ.Components.Stages.Maps;
using DQQ.Durations;
using DQQ.Enums;
using DQQ.Helper;
using DQQ.Pools;
using DQQ.Profiles.Durations;
using DQQ.Profiles.Durations.Debuffs;
using ReheeCmf.Helpers;
using ReheeCmf.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Skills.Attacks
{
  [Pooled]
  public class Rend : GeneralSkill
  {
    public override EnumDamageHand DamageHand => EnumDamageHand.MainHand;
    public override decimal CastTime => 0;
    public override decimal CoolDown => 1.5m;
    public override decimal DamageRate => 3m;

    public override bool CastWithWeaponSpeed => false;

    public override EnumSkill ProfileNumber => EnumSkill.Rend;

    public override string? Name => "撕裂";

    public override string? Discription => "使用武器撕裂敌人. 在5秒内造成住手武器伤害300%的流血伤害";


    protected override void DealingDamage(ITarget? caster, ITarget? skillTarget, DamageDeal[] damageDeals, IMap? map)
    {
      base.DealingDamage(caster, skillTarget, [], map);
    }
    protected override void AfterDealingDamage(ITarget? caster, ITarget? skillTarget, DamageTaken? damageTaken, IMap? map)
    {
      var rendDamage = CalculateDamage(caster, map);
      var durationParameter = new DurationParameter
      {
        Creator = caster,
        Value = rendDamage.Sum(b => b.DamagePoint),
        DurationSeconds = 5
      };
      var actualTarget = skillTarget ?? caster?.Target;
      DQQPool.TryGet<DurationProfile, EnumDurationNumber?>(EnumDurationNumber.Rend)?.CreateDuration(durationParameter, actualTarget, map);
    }
    
  }
}
