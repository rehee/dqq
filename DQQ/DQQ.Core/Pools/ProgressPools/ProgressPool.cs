using DQQ.Enums;
using DQQ.Profiles.Items;
using DQQ.Profiles.ZProgress;

namespace DQQ.Pools
{
  public static partial class DQQPool
  {
    public static Dictionary<EnumProgress, ProgressProfile> ProgressPool { get; set; } = new Dictionary<EnumProgress, ProgressProfile>();
  }
}
