using BootstrapBlazor.Components;
using DQQ.Components.Affixes;
using DQQ.Components.Items.Equips;
using DQQ.Components.Stages.Actors.Characters;
using DQQ.Entities;
using DQQ.Enums;
using DQQ.Profiles.Items.Equipments;
using DQQ.Web.Pages.DQQs.Items;
using DQQ.Web.Pages.DQQs.Items.Components;
using ReheeCmf.Commons.Jsons.Options;
using ReheeCmf.Helpers;
using ReheeCmf.Responses;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection.Metadata;
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
	
	
		public static async Task Equip(this OfflineCharacter character, ItemEntity? item,EnumEquipSlot? slot)
		{
			if (item == null)
			{
				return;
			}
			Func<Guid?, EnumEquipSlot?, Task<EquipProfile?>> queryItemType = async (id, slot) =>
			{
				await Task.CompletedTask;
				if(character?.SelectedCharacter?.EquipItems.TryGetValue(slot?? EnumEquipSlot.MainHand, out var itemget)==true)
				{
					return itemget?.EquipProfile;
				}
				return null;
			};
			Func<Guid?, EnumEquipSlot[], Task<ContentResponse<bool>>> unequipFunction = async (id, slots) =>
			{
				var result = new ContentResponse<bool>();
				result.SetSuccess(true);
				await character.UnEquip(slots);
				return result;
			};
			var result = await EquiptableHelper.UnequipBasedItem(item, character?.SelectedCharacter?.DisplayId, slot, queryItemType, unequipFunction);
			if (result.Success)
			{
				character?.SelectedCharacter?.EquipItems.TryAdd(result.Content, item);
			}
			var itemIndex = character?.Backpack?.FirstOrDefault(b => b.Id == item.Id);
			if (itemIndex != null)
			{
				try
				{
					character?.Backpack?.Remove(itemIndex);
				}
				catch
				{

				}
			}
		}
		public static Task UnEquip(this OfflineCharacter character, params EnumEquipSlot[] slots)
		{
			if (slots?.Any() == true)
			{
				foreach (var s in slots)
				{
					if(character?.SelectedCharacter?.EquipItems.TryRemove(s, out var outValue)==true)
					{
						if (outValue == null)
						{
							continue;
						}
						if (character.Backpack == null)
						{
							character.Backpack = new List<ItemEntity>();
						}
						character.Backpack.Add(outValue);
					}
				}
			}
			return Task.CompletedTask;
		}
	
		public static void TotalEquipProperty(this OfflineCharacter character)
		{
			if (character.SelectedCharacter == null)
			{
				return;
			}
			character.SelectedCharacter.Equips = new ConcurrentDictionary<EnumEquipSlot, Combats.IEquptment?>();

			if (character.SelectedCharacter.EquipItems != null)
			{
				foreach(var e in character.SelectedCharacter.EquipItems)
				{
					if (e.Value!=null && e.Value is ItemEntity itemEntity) 
					{
						var component = itemEntity.GenerateTypedComponent<EquipComponent>(null);
						character.SelectedCharacter.Equips.TryAdd(e.Key, component);
					}
					
				}
			}
			character.SelectedCharacter?.ResetCharacterCombatStatus();
			character.SelectedCharacter?.TotalEquipProperty();
		}

	}
}
