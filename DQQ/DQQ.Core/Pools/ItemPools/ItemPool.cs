using DQQ.Enums;
using DQQ.Profiles.Items;

namespace DQQ.Pools
{
  public static partial class DQQPool
  {
    public static Dictionary<EnumItem, ItemProfile> ItemPool { get; set; } = new Dictionary<EnumItem, ItemProfile>();
  }
}
