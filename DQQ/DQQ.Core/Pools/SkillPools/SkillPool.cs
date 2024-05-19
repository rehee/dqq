using DQQ.Enums;
using DQQ.Profiles.Durations;
using DQQ.Profiles.Skills;
using DQQ.Profiles.Skills.Attacks;

namespace DQQ.Pools
{
  public static partial class DQQPool
  {
    public static Dictionary<EnumSkill, ISkill> SkillPool { get; set; } = new Dictionary<EnumSkill, ISkill>();
    public static Dictionary<EnumDurationNumber, DurationProfile> DurationPool { get; set; } = new Dictionary<EnumDurationNumber, DurationProfile>();
  }
}
