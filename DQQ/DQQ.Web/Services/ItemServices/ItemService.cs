using Blazor.Serialization.Extensions;
using DQQ.Entities;
using DQQ.Enums;
using DQQ.Services.ItemServices;
using DQQ.Web.Services.Requests;
using ReheeCmf.Requests;
using ReheeCmf.Responses;

namespace DQQ.Web.Services.ItemServices
{
	public class ItemService : ClientServiceBase, IItemService
	{
		public ItemService(RequestClient<DQQGetHttpClient> client) : base(client)
		{
		}

		public async Task<IEnumerable<ItemEntity>?> ActorInventory(Guid? actorId)
		{
			return (await client.Request<IEnumerable<ItemEntity>?>(HttpMethod.Get, $"Items/Inventory/{actorId}")).Content;
		}

		public async Task<ContentResponse<bool>> EquipItem(Guid? actorId, Guid? itemId, EnumEquipSlot? slot)
		{
			return await client.Request<bool>(HttpMethod.Post, $"Items/Equip/{actorId}/{itemId}/{slot}");
		}

		public async Task<IEnumerable<ItemEntity>?> PickableItems(Guid? actorId)
		{
			return (await client.Request<IEnumerable<ItemEntity>?>(HttpMethod.Get, $"Items/Pick/{actorId}")).Content;


		}

		public async Task<ContentResponse<bool>> PickItem(Guid? actorId, params Guid[] itemId)
		{
			return await client.Request<bool>(HttpMethod.Post, $"Items/Pick/{actorId}", itemId.ToJson());
		}

		public async Task<ContentResponse<bool>> UnEquipItem(Guid? actorId, params EnumEquipSlot[] slots)
		{
			return await client.Request<bool>(HttpMethod.Post, $"Items/UnEquip/{actorId}", slots.ToJson());
		}
	}
}
