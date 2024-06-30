using DQQ.Components.Items;
using DQQ.Entities;
using DQQ.Profiles.Items.Currencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Helper
{
	public static class ItemEntityHelper
	{
		public static ItemEntity[] ConvertToSellItems(this IEnumerable<ItemEntity> items)
		{
			return items.GroupBy(b => b.Rarity).Select(b => (AbCurrency.New(b.Key), b.Count()))
				.Where(b => b.Item1 != null)
				.Select(b =>
				{
					var component = b.Item1!.GenerateComponent(new Random(), 1, b.Item2);
					return component.ToEntity();
				})
				.ToArray();
		}
	}
}
