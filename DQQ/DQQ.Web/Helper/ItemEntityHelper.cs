using BootstrapBlazor.Components;
using DQQ.Components.Affixes;
using DQQ.Entities;
using DQQ.Enums;
using DQQ.Web.Pages.DQQs.Items;
using DQQ.Web.Pages.DQQs.Items.Components;
using ReheeCmf.Commons.Jsons.Options;
using System.Collections.Concurrent;
using System.Text.Json;

namespace DQQ.Helper
{
	public static class ItemEntityHelper
	{
		public static async Task SetComponent(this ItemEntity? item, IComponentHtmlRenderer? renderer)
		{
			if (item == null || renderer == null)
			{
				return;
			}


			item.SetComponentString(await renderer!.RenderAsync<ItemComponent>(
				new Dictionary<string, object?>()
				{
					["Item"] = item,

				}));
		}

		public static async Task SetCompareComponent(this ItemEntity? item, ConcurrentDictionary<EnumEquipSlot, ItemEntity?>? mapper, IComponentHtmlRenderer? renderer)
		{
			if (item == null || renderer == null)
			{
				return;
			}
			item.SetCompareString(null);
			item.SetCompareStringAll(null);
			
			var avaliableSlots = item.GetAvaliableSlots()?.ToList();
			if (avaliableSlots?.Any() != true)
			{
				return;
			}
			if (item?.EquipType == EnumEquipType.TwoHandWeapon)
			{
				avaliableSlots.Add(EnumEquipSlot.OffHand);
			}
			
			var equipItems =
				avaliableSlots
				.Select(b =>
				{

					if (mapper?.TryGetValue(b, out var entity) == true)
					{
						return (b, entity);
					}
					return (b, null);
				})
				.Where(b => b.entity != null)
				.ToDictionary();

			var equipMapper = new Dictionary<EnumEquipSlot, string>();
			var compAll = await renderer!.RenderAsync<EquipCompare>(
				new Dictionary<string, object?>()
				{
					["EquipedItems"] = equipItems,
					["CurrentItem"] = item,
				});
			item!.SetCompareStringAll(compAll);

			foreach (var equip in equipItems)
			{
				var comp = await renderer!.RenderAsync<EquipCompare>(
				new Dictionary<string, object?>()
				{
					["EquipedItems"] = new Dictionary<EnumEquipSlot, ItemEntity?> { [equip.Key] = equip.Value },
					["CurrentItem"] = item,
					["Slot"] = equip.Key,
				});
				equipMapper.Add(equip.Key, comp);
			}
			item!.SetCompareString(equipMapper);
		}
	}
}
