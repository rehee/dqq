using DQQ.Enums;
using DQQ.Profiles.Mobs;
using DQQ.Profiles.PresetStrategies;

namespace DQQ.Pools
{
  public static partial class DQQPool
  {
    public static Dictionary<EnumPresetSkillStrategy, PresetStrategyProfile> PresetStrategyPool { get; set; } = [];
  }
}
