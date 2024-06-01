using DQQ.Enums;
using DQQ.Profiles.Affixes;
using DQQ.Profiles.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Pools
{
  public static partial class DQQPool
  {
    public static Dictionary<EnumAffixeNumber, AffixeProfile> AffixePool { get; set; } = new Dictionary<EnumAffixeNumber, AffixeProfile>();
  }
}
