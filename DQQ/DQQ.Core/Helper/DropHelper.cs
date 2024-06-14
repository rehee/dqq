using DQQ.Components.Items;
using DQQ.Components.Stages.Actors;
using DQQ.Components.Stages.Maps;
using DQQ.Consts;
using DQQ.Drops;
using DQQ.Enums;
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
				var random = RandomHelper.GetRandom(map?.TickParameter?.Random, 0, 100);
				if (dropRate < random)
				{
					break;
				}
				var dropRarity = RandomHelper.GetRandom(map?.TickParameter?.Random, 0, 10001);
				var lists = DQQPool.ItemPool.Select(b => b.Value).Where(b => (b.Rarity * mapRarityRate) >= dropRarity).OrderByDescending(b => b.Rarity).ToList();
				var itemProfile = lists.GetRamdom(map?.TickParameter?.Random);
				if (itemProfile == null)
				{
					continue;
				}
				var item = itemProfile.GenerateComponent(map?.TickParameter?.Random, map?.MapLevel, (int)Math.Round(mapRate * itemProfile.DropQuantity, 0), GetItemRarity(map?.TickParameter?.Random));
				if (!item.Avaliable)
				{
					continue;
				}
				list.Add(item);
			}
			return list.ToArray();
		}

		public static EnumRarity GetItemRarity(Random r)
		{
			var random = RandomHelper.GetRandom(r, 0, 1000001);
			if (random <= DQQGeneral.RareRate)
			{
				return EnumRarity.Rare;
			}
			if (random <= DQQGeneral.MagicRate)
			{
				return EnumRarity.Magic;
			}
			return EnumRarity.Normal;
		}
	}
}
