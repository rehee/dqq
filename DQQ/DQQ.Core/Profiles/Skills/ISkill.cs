using DQQ.Commons;
using DQQ.Components.Stages;
using DQQ.Components.Stages.Maps;
using DQQ.Enums;
using ReheeCmf.Responses;

namespace DQQ.Profiles.Skills
{
  public interface ISkill
  {
    bool NoPlayerSkill { get; }
    EnumSkill SkillNumber { get; }
    string? SkillName { get; }
    decimal CastTime { get; }
    bool CastWithWeaponSpeed { get; }
    decimal CoolDown { get; }
    decimal DamageRate { get; }
    string? Discription { get; }
    Task<ContentResponse<bool>> CastSkill(ITarget? caster, ITarget? skillTarget, IEnumerable<ITarget>? target, IMap? map);
  }
}
