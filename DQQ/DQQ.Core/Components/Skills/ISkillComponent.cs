using DQQ.Commons;
using DQQ.Components.Stages;
using DQQ.Components.Stages.Maps;
using DQQ.Profiles.Skills;
using DQQ.Tags;
using ReheeCmf.Responses;

namespace DQQ.Components.Skills
{
  public interface ISkillComponent : ITagged, IDQQComponent
  {
    decimal CastTime { get; }
    decimal Cooldown { get; }
    decimal DamageRate { get; }
    ISkill? SkillProfile { get; }
    Task<ContentResponse<bool>> OnTick(ITarget? caster, IEnumerable<ITarget>? targets, IMap? map);
  }
}
