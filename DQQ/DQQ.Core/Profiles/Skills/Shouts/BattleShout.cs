using DQQ.Attributes;
using DQQ.Commons;
using DQQ.Components.Parameters;
using DQQ.Components.Stages;
using DQQ.Components.Stages.Maps;
using DQQ.Durations;
using DQQ.Enums;
using DQQ.Pools;
using DQQ.Profiles.Durations;
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
    public override EnumSkillCategory Category => EnumSkillCategory.Strategy;
		public override bool NoPlayerSkill => false;
    public override decimal CastTime => 0;
    public override decimal CoolDown => 15m;
    public override decimal DamageRate => 0;
    public override bool CastWithWeaponSpeed => false;
    public override EnumSkillNumber ProfileNumber => EnumSkillNumber.BattleShout;
    public override string? Name => "战吼";
    protected override bool SelfTarget => true;
    public override string? Discription => "发出战吼激励友方. 受此影响攻击力增加10%. 持续120秒";

    protected override async Task DealingDamage(ComponentTickParameter? parameter, DamageDeal[] damageDeals, IMap? map)
    {
      await Task.CompletedTask;
    }

    public override async Task<ContentResponse<bool>> CastSkill(ComponentTickParameter? parameter)
    {
      var result = await base.CastSkill(parameter);
      if (!result.Success)
      {
        return result;
      }
      DQQPool.TryGet<DurationProfile, EnumDurationNumber?>(EnumDurationNumber.BattleShout)?.CreateDuration(new DurationParameter
      {
        Creator = parameter?.From,
        DurationSeconds = 120,
        Value = 0
      }, parameter?.From, parameter?.Map);
      return result;
    }
  }
}
