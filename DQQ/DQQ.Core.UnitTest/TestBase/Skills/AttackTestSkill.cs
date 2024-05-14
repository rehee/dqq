using DQQ.Commons;
using DQQ.Components.Skills;
using DQQ.Components.Stages;
using DQQ.Components.Stages.Maps;
using DQQ.Entities;
using ReheeCmf.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.UnitTest.TestBase.Skills
{
  public class AttackTestSkill : SkillComponent
  {
    public async Task<bool> CastSkill(ITarget? caster, IEnumerable<ITarget>? targets, IMap? map)
    {
      await Task.CompletedTask;
      if (caster?.Target == null || caster?.Target.Alive != true)
      {
        return false;
      }
      caster.Target.TakeDamage(caster, 100, null);
      return true;
    }
    public override async Task<ContentResponse<Boolean>> OnTick(ITarget? caster, IEnumerable<ITarget>? targets, IMap? map)
    {
      var result = await base.OnTick(caster, targets, map);
      if (result.Success)
      {
        await CastSkill(caster, targets, map);

      }
      return result;
    }
    public override void Initialize(IDQQEntity profile)
    {
      throw new NotImplementedException();
    }
  }
}
