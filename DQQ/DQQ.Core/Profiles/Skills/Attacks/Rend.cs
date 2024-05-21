using DQQ.Attributes;
using DQQ.Components.Stages;
using DQQ.Components.Stages.Maps;
using DQQ.Durations;
using DQQ.Enums;
using DQQ.Helper;
using DQQ.Pools;
using DQQ.Profiles.Durations.Debuffs;
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
    public override decimal CastTime => 0;

    public override decimal CoolDown => 1.5m;

    public override decimal DamageRate => 0;

    public override bool CastWithWeaponSpeed => false;

    public override EnumSkill ProfileNumber => EnumSkill.Rend;

    public override string? Name => "撕裂";

    public override string? Discription => "使用武器撕裂敌人. 在5秒内造成住手武器伤害300%的流血伤害";

    public override long CalculateDamage(ITarget? caster, IMap? map)
    {
      return 0;
    }
    protected override void TakeDamage(ITarget? caster, ITarget? skillTarget, long damage, IMap? map)
    {
      
    }
    public override async Task<ContentResponse<bool>> CastSkill(ITarget? caster, ITarget? skillTarget, IEnumerable<ITarget>? target, IMap? map)
    {
      var result = await base.CastSkill(caster, skillTarget, target, map);
      var actualTarget = skillTarget ?? caster?.Target;

      if (caster?.MainHand <= 0)
      {
        return result;
      }
      var rendDamage = caster?.MainHand.Percentage(3m);
      var durationParameter = new DurationParameter
      {
        Creator = caster,
        Value = rendDamage ?? 0,
        DurationSeconds = 5
      };
      DQQPool.DurationPool[EnumDurationNumber.Rend].CreateDuration(durationParameter, actualTarget, map);
      return result;
    }
  }
}
