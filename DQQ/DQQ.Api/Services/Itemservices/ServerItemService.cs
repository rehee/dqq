using DQQ.Entities;
using DQQ.Enums;
using DQQ.Services.ItemServices;
using Microsoft.EntityFrameworkCore;
using ReheeCmf.Contexts;
using ReheeCmf.Helpers;
using ReheeCmf.Responses;
using DQQ.Helper;
using System.Linq;
using DQQ.Pools;
using Microsoft.Linq.Translations;
using DQQ.Profiles.Items;
using DQQ.Services.ActorServices;
using Humanizer;
using DQQ.Profiles.Items.Currencies;
using static Grpc.Core.Metadata;
using DQQ.Profiles.Items.Equipments;

namespace DQQ.Api.Services.Itemservices
{
	public class ServerItemService : IItemService
	{
		private readonly IContext context;
		private readonly ITemporaryService tiService;
		private readonly ICharacterService characterService;

		public ServerItemService(IContext context, ITemporaryService tiService, ICharacterService characterService)
		{
			this.context = context;
			this.tiService = tiService;
			this.characterService = characterService;
		}

		protected IQueryable<ItemEntity> QueryItemEntity(Guid? actorId, bool asNoTracking)
		{
			return context.Query<ItemEntity>(asNoTracking).Where(b => b.ActorId == actorId && b.IsEquipped != true).WithTranslations();
		}

		public async Task<IEnumerable<ItemEntity>?> ActorInventory(Guid? actorId)
		{
			await Task.CompletedTask;
			if (actorId == null)
			{
				return null;
			}
			return await QueryItemEntity(actorId,true).ToArrayAsync();
		}

		public async Task<ContentResponse<bool>> DropBackpackItem(Guid? actorId, params Guid[] itemId)
		{
			var result = new ContentResponse<bool>();
			var actor = await characterService.GetCharacter(actorId);
			if (itemId.Any() != true ||actor == null)
			{
				return result;
			}
			

			var itemReadyForDelete = await QueryItemEntity(actorId, false).Where(b=> itemId.Contains(b.Id)).ToArrayAsync();
			try
			{
				foreach (var p in itemReadyForDelete)
				{
					context.Delete(p);
				}
			}
			catch (Exception ex) 
			{
				var d = 1;
			}
			
			await context.SaveChangesAsync();
			result.SetSuccess(true);
			return result;
		}

		public async Task<ContentResponse<bool>> DropPickupItem(Guid? actorId, params Guid[] itemId)
		{
			var result = new ContentResponse<bool>();
			var actor = await characterService.GetCharacter(actorId);
			if (actor == null)
			{
				return result;
			}
			await tiService.PickAndRemoveTemporaryItems(actorId, itemId);
			result.SetSuccess(true);
			return result;
		}

		public async Task<ContentResponse<bool>> EquipItem(Guid? actorId, Guid? itemId, EnumEquipSlot? slot)
		{
			return await equipItem(true, actorId, itemId, slot);
		}
		public async Task<ContentResponse<bool>> equipItem(bool saveAtEnd, Guid? actorId, Guid? itemId, EnumEquipSlot? slot)
		{
			var result = new ContentResponse<bool>();
			try
			{
				if (actorId == null || itemId == null)
				{
					return result;
				}
				var item = await context.Query<ItemEntity>(false).Where(b => b.Id == itemId && b.ActorId == actorId).FirstOrDefaultAsync();
				

				Func<Guid?, EnumEquipSlot?, Task<EquipProfile?>> queryItemProfile = async (id, slot) => await context.Query<ActorEquipmentEntity>(true).Where(b => b.ActorId == id && b.EquipSlot == slot).Select(b => b.Item.EquipProfile).FirstOrDefaultAsync();
				var unequipResult = await EquiptableHelper.UnequipBasedItem(item,actorId,slot,queryItemProfile,(g,s)=> unEquipItem(false,g,s));
				if (unequipResult?.Success != true)
				{
					return result;
				}
				await context.SaveChangesAsync();
				var newEquip = new ActorEquipmentEntity()
				{
					ActorId = actorId,
					ItemId = itemId,
					EquipSlot = unequipResult.Content,
				};
				await context.AddAsync(newEquip);
				if (saveAtEnd)
				{
					await context.SaveChangesAsync();
				}
				result.SetSuccess(true);
			}
			catch (Exception ex)
			{
				result.SetError(ex);
			}

			return result;
		}
		public async Task<ContentResponse<bool>> EquipItems(Guid? actorId, params (EnumEquipSlot Slot, Guid? Id)[] items)
		{
			var result = new ContentResponse<bool>();

			foreach (var item in items)
			{
				await equipItem(false, actorId, item.Id, item.Slot);
			}
			result.SetSuccess(true);
			return result;
		}

		public async Task<IEnumerable<ItemEntity>?> PickableItems(Guid? actorId)
		{
			if (actorId == null)
			{
				return null;
			}
			var items = await tiService.GetAllTemporaryItems(actorId);
			var itemIds = items.Select(b => b.Id).ToHashSet();
			var itemsAlreadyExisting = await context.Query<ItemEntity>(true).Where(b => itemIds.Contains(b.Id)).Select(b => b.Id).ToArrayAsync();
			var itemNeedRemove = items.Where(b => itemsAlreadyExisting.Contains(b.Id)).Select(b => b.Id).ToArray();
			await tiService.PickAndRemoveTemporaryItems(actorId, itemNeedRemove);
			return items.Where(b => !itemsAlreadyExisting.Contains(b.Id)).ToArray();
		}

		public async Task<ContentResponse<bool>> PickItem(Guid? actorId, params Guid[] itemId)
		{
			var result = new ContentResponse<bool>();
			var actor = await characterService.GetCharacter(actorId);
			if (actor == null || itemId?.Any() != true)
			{
				return result;
			}
			var limits = actor.GetInventoryBackpackLimit();
			var currentItem = await QueryItemEntity(actorId, true).Select(b => b.Id).CountAsync();
			var avaliableItem = limits - currentItem;
			if (avaliableItem <= 0)
			{
				return result;
			}
			var foundItem = await context.Query<ItemEntity>(true).Where(b => itemId.Take(avaliableItem).Any(i => b.Id == i)).AnyAsync();
			if (foundItem)
			{
				result.SetValidation(ValidationResultHelper.New("One of Item already picked"));
				return result;

			}
			var items = await tiService.PickAndRemoveTemporaryItems(actorId, itemId);
			if (items?.Any() != true)
			{
				result.SetError(System.Net.HttpStatusCode.NotFound);
				return result;
			}

			foreach (var item in items.GroupBy(b => b.ItemNumber))
			{
				var itemProfile = DQQPool.TryGet<ItemProfile, EnumItem?>(item.Key);
				if (itemProfile == null)
				{
					continue;
				}
				if (itemProfile.IsStack)
				{
					var sum = item.Sum(b => b.Quantity ?? 0);
					if (sum == 0)
					{
						continue;
					}
					var existingItem = await context.Query<ItemEntity>(false).Where(b => b.ActorId == actorId && b.ItemNumber == item.Key).FirstOrDefaultAsync();
					if (existingItem != null)
					{
						existingItem.Quantity = existingItem.Quantity + sum;
					}
					else
					{
						var first = item.FirstOrDefault();
						first.Quantity = sum;
						first.ActorId = actorId;
						await context.AddAsync(first);
					}
				}
				else
				{
					foreach (var subItem in item)
					{
						subItem.ActorId = actorId;
						await context.AddAsync(subItem);
					}
				}
			}
			try
			{
				await context.SaveChangesAsync();
				result.SetSuccess(true);
			}
			catch (Exception ex)
			{
				result.SetError(ex);

			}
			return result;
		}

		public async Task<ContentResponse<bool>> SellBackpackItem(Guid? actorId, params Guid[] itemId)
		{
			var result = new ContentResponse<bool>();
			if (itemId.Any() == false)
			{
				return result;
			}
			var actor = await characterService.GetCharacter(actorId);
			if (actor == null)
			{
				return result;
			}
			var item = (await QueryItemEntity(actorId, false).Where(b=> itemId.Contains(b.Id)).ToArrayAsync()).Where(b => b.ItemType != EnumItemType.Currency).ToArray();
			var items = item.GroupBy(b => b.Rarity).Select(b => (AbCurrency.New(b.Key), b.Count()))
				.Where(b => b.Item1 != null)
				.Select(b =>
				{
					var component = b.Item1!.GenerateComponent(new Random(), 1, b.Item2);
					return component.ToEntity();
				})
				.ToArray();
			if (items?.Any()!=true)
			{
				return result;
			}
			foreach(var entity in items)
			{
				var existingItem = await QueryItemEntity(actorId,false).Where(b => b.ItemNumber == entity.ItemNumber).FirstOrDefaultAsync();
				if(existingItem!= null)
				{
					existingItem.Quantity = existingItem.Quantity + entity.Quantity;
				}
				else
				{
					entity.ActorId = actorId;
					await context.AddAsync(entity);
				}
			}
			foreach(var deletedItem in item)
			{
				context.Delete(deletedItem);
			}
			await context.SaveChangesAsync();
			return result;
		}

		public async Task<ContentResponse<bool>> UnEquipItem(Guid? actorId, params EnumEquipSlot[] slots)
		{
			return await unEquipItem(true, actorId, slots);
		}
		private async Task<ContentResponse<bool>> unEquipItem(bool save, Guid? actorId, params EnumEquipSlot[] slots)
		{
			var result = new ContentResponse<bool>();

			if (actorId == null || slots?.Any() != true)
			{
				result.SetSuccess(true);
				return result;
			}

			var list = slots.ToArray();
			var existingEquip = await context.Query<ActorEquipmentEntity>(false).Where(b =>
						b.ActorId == actorId && (b.EquipSlot == null || slots.Contains(b.EquipSlot.Value)))
					.ToArrayAsync();
			foreach (var e in existingEquip)
			{
				context.Delete<ActorEquipmentEntity>(e);
			}
			if (save)
			{
				await context.SaveChangesAsync();
				result.SetSuccess(true);
			}
			else
			{
				result.SetSuccess(true);
			}

			return result;
		}
	}
}
