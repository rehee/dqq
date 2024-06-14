using DQQ.Enums;
using DQQ.Profiles.Mobs;

namespace DQQ.Pools
{
  public static partial class DQQPool
  {
    public static Dictionary<EnumMob, AbMobProfile> MobPool { get; set; } = new Dictionary<EnumMob, AbMobProfile>();
  }
}
