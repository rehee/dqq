using DQQ.Enums;
using DQQ.Profiles.Durations;
using DQQ.Profiles.Skills;
using DQQ.Profiles.Skills.Attacks;

namespace DQQ.Pools
{
  public static partial class DQQPool
  {
    public static Dictionary<EnumSkillNumber, SkillProfile> SkillPool { get; set; } = new Dictionary<EnumSkillNumber, SkillProfile>();
    public static Dictionary<EnumDurationNumber, DurationProfile> DurationPool { get; set; } = new Dictionary<EnumDurationNumber, DurationProfile>();

    
  }
}
