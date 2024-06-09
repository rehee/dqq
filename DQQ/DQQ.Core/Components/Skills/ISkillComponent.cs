using DQQ.Commons;
using DQQ.Components.Stages;
using DQQ.Components.Stages.Maps;
using DQQ.Enums;
using DQQ.Profiles.Skills;
using DQQ.Strategies.SkillStrategies;
using DQQ.Tags;
using ReheeCmf.Responses;

namespace DQQ.Components.Skills
{
  public interface ISkillComponent : ITagged, IDQQComponent
  {
    EnumSkillSlot Slot { get; set; }
    decimal CastTime { get; }
    decimal Cooldown { get; }
    decimal DamageRate { get; }
    ISkill? SkillProfile { get; }
    SkillStrategy[]? SkillStrategies { get; }
    int TotalCount { get; set; }
    int WaveCount { get; set; }
  }
}
