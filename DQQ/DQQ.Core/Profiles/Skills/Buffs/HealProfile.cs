using DQQ.Attributes;
using DQQ.Components.Stages;
using DQQ.Components.Stages.Maps;
using DQQ.Enums;
using DQQ.Helper;
using ReheeCmf.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Skills.Buffs
{
  [Pooled]
  public class HealProfile : SkillProfile
  {
    public override bool NoPlayerSkill => false;

    public override decimal CastTime => 0m;

    public override decimal CoolDown => 30m;

    public override decimal DamageRate => 0m;

    public override bool CastWithWeaponSpeed => false;

    public override EnumSkill ProfileNumber => EnumSkill.Healing;

    public override string? Name => "快速治疗";

    public override string? Discription => "快速治疗自身. 回复最大生命60%的生命";
    public override long CalculateDamage(ITarget? caster, IMap? map)
    {
      return 0;
    }
    protected override void TakeDamage(ITarget? caster, ITarget? skillTarget, long damage, IMap? map)
    {

    }
    public override async Task<ContentResponse<bool>> CastSkill(ITarget? caster, ITarget? skillTarget, IEnumerable<ITarget>? target, IMap? map)
    {

      var result = await base.CastSkill(caster, caster, target, map);
      if (result.Success)
      {
        var casterHealHp = (long)(caster.MaximunLife * 0.6m);
        caster.TakeHealing(caster, casterHealHp, map, this);
      }
      
      
      return result;
    }
  }
}
