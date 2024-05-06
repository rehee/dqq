using DQQ.Enums;
using DQQ.Profiles.Mobs;

namespace DQQ.Pools
{
  public static partial class DQQPool
  {
    public static Dictionary<EnumMob, MobProfile> MobPool { get; set; } = new Dictionary<EnumMob, MobProfile>();
  }
}
