using DQQ.Commons;
using DQQ.Components.Skills;
using DQQ.Components.Stages;
using DQQ.Components.Stages.Maps;
using DQQ.Entities;
using ReheeCmf.Responses;

namespace DQQ.UnitTest.TestBase.Skills
{
  public class TestSkill : SkillComponent
  {
    public int CastTimeCount { get; set; }
    public async Task<Boolean> CastSkill(ITarget? caster, IEnumerable<ITarget>? targets, IMap? map)
    {
      await Task.CompletedTask;
      CastTimeCount++;
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
