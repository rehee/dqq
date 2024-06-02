using DQQ.Commons;
using DQQ.Components.Parameters;
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
    public async Task<bool> CastSkill(ComponentTickParameter? parameter)
    {
      await Task.CompletedTask;
      if (parameter?.SelectedTarget?.Alive != true)
      {
        return false;
      }
      parameter!.SelectedTarget!.TakeDamage(parameter.From, [DamageDeal.New(100)], null, null);
      return true;
    }
    public override async Task<ContentResponse<Boolean>> OnTick(ComponentTickParameter? parameter)
    {
      var result = await base.OnTick(parameter);
      if (result.Success)
      {
        await CastSkill(parameter);

      }
      return result;
    }
    public override void Initialize(IDQQEntity profile)
    {
      throw new NotImplementedException();
    }
  }
}
