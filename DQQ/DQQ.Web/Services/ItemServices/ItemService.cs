using DQQ.Api.Services.Itemservices;
using DQQ.Combats;
using DQQ.Consts;
using DQQ.Entities;
using DQQ.Enums;
using DQQ.Services;
using DQQ.Services.ItemServices;
using DQQ.Web.Datas;
using DQQ.Web.Services.Requests;
using ReheeCmf.Helpers;
using ReheeCmf.Requests;
using ReheeCmf.Responses;

namespace DQQ.Web.Services.ItemServices
{
	public class ItemService : ClientServiceBase, IItemService
	{
		private readonly ITemporaryService temporaryService;

		public ItemService(ITemporaryService temporaryService, RequestClient<DQQGetHttpClient> client, IIndexRepostory repostory, IGameStatusService statusService) : base(client, repostory, statusService)
		{
			this.temporaryService = temporaryService;
		}

		public async Task<IEnumerable<ItemEntity>?> ActorInventory(Guid? actorId)
		{
			if(await IsOnleService())
			{
				return (await client.Request<IEnumerable<ItemEntity>?>(HttpMethod.Get, $"Items/Inventory/{actorId}")).Content;
			}
			return (await Repostory.GetCurrentOfflineCharacter(actorId))?.Backpack;
		}

		public async Task<ContentResponse<bool>> DropBackpackItem(Guid? actorId, params Guid[] itemId)
		{
			if (await IsOnleService())
			{
				return await client.Request<bool>(HttpMethod.Delete, $"Items/Drop/Backpack/{actorId}", itemId.ToJson());
			}
			return await Repostory.Update<OfflineCharacter>(actorId, c => Task.Run(() =>
			{
				if (c == null)
				{
					return Task.CompletedTask;
				}
				if (c.Backpack == null)
				{
					c.Backpack = new List<ItemEntity>();
					return Task.CompletedTask;
				}
				c.Backpack = c.Backpack.Where(b => !itemId.Contains(b.Id)).ToList();
				return Task.CompletedTask;
			}));
		}

		public async Task<ContentResponse<bool>> DropPickupItem(Guid? actorId, params Guid[] itemId)
		{
			if(await IsOnleService())
			{
				return await client.Request<bool>(HttpMethod.Delete, $"Items/Drop/Pickup/{actorId}", itemId.ToJson());
			}
			var result = new ContentResponse<bool>();
			await temporaryService.PickAndRemoveTemporaryItems(actorId, itemId);
			result.SetSuccess(true);
			return result;
		}

		public async Task<ContentResponse<bool>> EquipItem(Guid? actorId, Guid? itemId, EnumEquipSlot? slot)
		{
			if(await IsOnleService())
			{
				return await client.Request<bool>(HttpMethod.Post, $"Items/Equip/{actorId}/{itemId}/{slot}");
			}
			return await Repostory.Update<OfflineCharacter>(actorId, c => Task.Run(async () =>
			{
				var item = c.Backpack?.FirstOrDefault(b => b.Id == itemId);
				if (item == null)
				{
					return;
				}
				c.Backpack?.Remove(item);
				await c.Equip(item, slot);
				c.TotalEquipProperty();
			}));
		}

		public Task<ContentResponse<bool>> EquipItems(Guid? actorId, params (EnumEquipSlot Slot, Guid? Id)[] items)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<ItemEntity>?> PickableItems(Guid? actorId)
		{
			if(await IsOnleService())
			{
				return (await client.Request<IEnumerable<ItemEntity>?>(HttpMethod.Get, $"Items/Pick/{actorId}")).Content;
			}
			return await temporaryService.GetAllTemporaryItems(actorId);

		}

		public async Task<ContentResponse<bool>> PickItem(Guid? actorId, params Guid[] itemId)
		{
			if (await IsOnleService()) 
			{
				return await client.Request<bool>(HttpMethod.Post, $"Items/Pick/{actorId}", itemId.ToJson());
			}
			var result = new ContentResponse<bool>();
			var character = await Repostory.GetCurrentOfflineCharacter(actorId);
			if (character == null)
			{
				return result;
			}

			var max = character.SelectedCharacter?.GetInventoryBackpackLimit() ?? DQQGeneral.BasicLevelUpExp;
			var current = character?.Backpack?.Count() ?? 0;
			if (current >= max)
			{
				return result;
			}
			if(character.Backpack == null)
			{
				character.Backpack = new List<ItemEntity>();
			}
			var pick = itemId.Take(max - current).ToArray();
			var pickItems = await temporaryService.PickAndRemoveTemporaryItems(actorId, pick);
			if (pickItems?.Any() != true)
			{
				return result;
			}
			foreach(var pi in pickItems)
			{
				pi.IsTempItem = false;
			}
			foreach (var pi in pickItems.Where(b=>b.Profile?.IsStack!=true))
			{
				pi.IsTempItem = false;
				character.Backpack.Add(pi);
			}

			foreach (var pi in pickItems.Where(b => b.Profile?.IsStack == true).GroupBy(b=>b.ItemNumber)
				.Select(b=>
				{
					var first = b.FirstOrDefault();
					first.Quantity = b.Sum(b => b.Quantity);
					return first!;
				}))
			{
				var existing = character.Backpack.FirstOrDefault(b1 => b1.ItemNumber == pi.ItemNumber);
				if (existing!=null)
				{
					existing.Quantity = existing.Quantity + pi.Quantity;
				}
				else
				{
					character.Backpack.Add(pi);
				}
			}
			await Repostory.Update<OfflineCharacter>(character.Id, c =>
			{
				c.Backpack = character.Backpack.DistinctBy(b=>b.Id).ToList();
			});
			return result;
		}

		public async Task<ContentResponse<bool>> SellBackpackItem(Guid? actorId, params Guid[] itemId)
		{
			if(await IsOnleService())
			{
				return await client.Request<bool>(HttpMethod.Post, $"Items/Sell/Backpack/{actorId}", itemId.ToJson());
			}
			return await Repostory.Update<OfflineCharacter>(actorId, c => Task.Run(async () =>
			{
				await Task.CompletedTask;
				if (c == null)
				{
					return;
				}
				if(c.Backpack== null)
				{
					c.Backpack = new List<ItemEntity>();
					return;
				}

				var deletedItems = c.Backpack.Where(b => b.Profile?.IsStack!= true && itemId.Contains(b.Id)).ToArray();
				c.Backpack = c.Backpack.Where(b => !(b.Profile?.IsStack != true && itemId.Contains(b.Id))).ToList();
				var sellItems = deletedItems.ConvertToSellItems();

				foreach (var item in sellItems) 
				{
					var existingItem = c.Backpack.FirstOrDefault(b => b.ItemNumber == item.ItemNumber);
					if (existingItem != null) 
					{
						existingItem.Quantity = item.Quantity+ existingItem.Quantity;
					}
					else
					{
						c.Backpack.Add(item);
					}
				}

			}));
		}

		public async Task<ContentResponse<bool>> UnEquipItem(Guid? actorId, params EnumEquipSlot[] slots)
		{
			if(await IsOnleService())
			{
				return await client.Request<bool>(HttpMethod.Post, $"Items/UnEquip/{actorId}", slots.ToJson());
			}

			return await Repostory.Update<OfflineCharacter>(actorId, (c)=> Task.Run(async () =>
			{
				await Task.CompletedTask;
				if (c == null)
				{
					return;
				}
				await c.UnEquip(slots);
				c.SelectedCharacter.CombatPanel = new CombatPanel();
				c.SelectedCharacter.CombatPanel.StaticPanel.MaximunLife = 50;
				c.TotalEquipProperty();
			}));
		}
	}
}
