using DQQ.Components.Items;
using DQQ.Components.Stages.Actors;
using DQQ.Components.Stages.Maps;
using DQQ.Drops;
using DQQ.Pools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Helper
{
  public static class DropHelper
  {
    public static IEnumerable<ItemComponent> Drop(this IDropper dropper, IMap? map, decimal? additionalRate = 1m)
    {
      var list = new List<ItemComponent>();
      var mapRate = (1m + (map?.DropQuantity ?? 0m));
      var mapRarityRate = (1m + (map?.DropQuality ?? 0m));
      var dropRate = dropper.DropRate * mapRate * additionalRate;
      for (var i = 0; i < DropConst.MaxDropNumber; i++)
      {
        var random = RandomHelper.GetRandom(0, 100);
        if (dropRate >= random)
        {
          var dropRarity = RandomHelper.GetRandom(0, 10000);
          var lists = DQQPool.ItemPool.Select(b => b.Value).Where(b => (b.Rarity * mapRarityRate) >= dropRarity).OrderByDescending(b => b.Rarity).ToList();



          var itemProfile = lists.GetRamdom();
          if (itemProfile == null)
          {
            continue;
          }
          var item = itemProfile.GenerateComponent(map?.MapLevel, (int)Math.Round(mapRate * itemProfile.DropQuantity, 0));
          list.Add(item);
        }
      }
      return list.ToArray();
    }
  }
}
