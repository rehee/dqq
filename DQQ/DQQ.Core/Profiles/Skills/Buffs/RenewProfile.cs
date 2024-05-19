﻿using DQQ.Attributes;
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

namespace DQQ.Profiles.Skills.Buffs
{
  [Pooled]
  public class RenewProfile : SkillProfile
  {
    public override bool NoPlayerSkill => false;

    public override decimal CastTime => 0m;

    public override decimal CoolDown => 5m;

    public override decimal DamageRate => 0m;

    public override bool CastWithWeaponSpeed => false;

    public override EnumSkill ProfileNumber => EnumSkill.Renew;

    public override string? Name => "回复";

    public override string? Discription => "周期性的持续回复生命";
    public override long CalculateDamage(ITarget? caster, IMap? map)
    {
      return 0;
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
        DurationSeconds = 5
      };
      DQQPool.DurationPool[EnumDurationNumber.Renew].CreateDuration(durationParameter, caster, map);
      return result;
    }
  }
}
