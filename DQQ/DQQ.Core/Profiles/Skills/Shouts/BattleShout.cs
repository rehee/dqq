using DQQ.Attributes;
using DQQ.Components.Stages;
using DQQ.Components.Stages.Maps;
using DQQ.Durations;
using DQQ.Enums;
using DQQ.Pools;
using ReheeCmf.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Skills.Shouts
{
  [Pooled]
  public class BattleShout : SkillProfile
  {
    public override bool NoPlayerSkill => false;
    public override decimal CastTime => 0;
    public override decimal CoolDown => 15m;
    public override decimal DamageRate => 0;
    public override bool CastWithWeaponSpeed => false;
    public override EnumSkill ProfileNumber => EnumSkill.BattleShout;
    public override string? Name => "战吼";
    protected override bool SelfTarget => true;
    public override string? Discription => "发出战吼激励友方. 受此影响攻击力增加10%. 持续120秒";

    protected override void TakeDamage(ITarget? caster, ITarget? skillTarget, long damage, IMap? map)
    {

    }

    public override async Task<ContentResponse<bool>> CastSkill(ITarget? caster, ITarget? skillTarget, IEnumerable<ITarget>? target, IMap? map)
    {
      var result = await base.CastSkill(caster, caster, target, map);
      if (!result.Success)
      {
        return result;
      }
      DQQPool.DurationPool[EnumDurationNumber.BattleShout].CreateDuration(new DurationParameter
      {
        Creator = caster,
        DurationSeconds = 120,
        Value = 0
      }, caster, map);
      return result;
    }
  }
}
