using DQQ.Attributes;
using DQQ.Components.Stages;
using DQQ.Components.Stages.Maps;
using DQQ.Enums;
using ReheeCmf.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Skills.NonPlayerSkills
{
  [Pooled]
  public class HatefulStrike : NonPlayerSKill
  {
    public override decimal CastTime => 0m;

    public override decimal CoolDown => 15m;

    public override decimal DamageRate => 10m;

    public override bool CastWithWeaponSpeed => false;

    public override EnumSkill ProfileNumber => EnumSkill.HatefulStrike;

    public override string? Name => "仇恨打击";
    public override string? Discription => "仇恨打击";
    public override Task<ContentResponse<bool>> CastSkill(ITarget? caster, ITarget? skillTarget, IEnumerable<ITarget>? target, IMap? map)
    {
      return base.CastSkill(caster, skillTarget, target, map);
    }
  }
}
