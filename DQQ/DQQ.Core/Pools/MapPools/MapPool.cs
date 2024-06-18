using DQQ.Enums;
using DQQ.Profiles.Maps;
using DQQ.Profiles.Mobs;

namespace DQQ.Pools
{
  public static partial class DQQPool
  {
    public static Dictionary<EnumMapNumber, MapProfile> MapPools { get; set; } = new Dictionary<EnumMapNumber, MapProfile>();
  }
}
