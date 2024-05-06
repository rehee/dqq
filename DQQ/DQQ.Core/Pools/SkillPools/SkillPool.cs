using DQQ.Enums;
using DQQ.Profiles.Skills;
using DQQ.Profiles.Skills.Attacks;

namespace DQQ.Pools
{
  public static partial class DQQPool
  {
    public static Dictionary<EnumSkill, ISkill> SkillPool { get; set; } = new Dictionary<EnumSkill, ISkill>();
  }
}
