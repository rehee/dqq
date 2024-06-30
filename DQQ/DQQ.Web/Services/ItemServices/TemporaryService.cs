using DQQ.Api.Services.Itemservices;
using DQQ.Components.Items;
using DQQ.Consts;
using DQQ.Entities;
using DQQ.Services;
using DQQ.Web.Datas;
using DQQ.Web.Services.Requests;
using ReheeCmf.Helpers;
using ReheeCmf.Requests;
using ReheeCmf.Responses;
using System.Linq;

namespace DQQ.Web.Services.ItemServices
{
	public class TemporaryService : ClientServiceBase, ITemporaryService
	{
		public TemporaryService(RequestClient<DQQGetHttpClient> client, IIndexRepostory repostory, IGameStatusService statusService) : base(client, repostory, statusService)
		{
		}

		public async Task<ContentResponse<bool>> AddAndIntoTemporary(Guid? actorId, params ItemComponent[] items)
		{
			if(await IsOnleService())
			{
				throw new NotImplementedException();
			}
			var character = await Repostory.GetCurrentOfflineCharacter(actorId);
			var result = new ContentResponse<bool>();
			if (character == null) 
			{
				return result;
			}
			var maxNumber = character?.SelectedCharacter?.GetInventoryPickupLimit()?? DQQGeneral.InventoryPickupLimit;

			if (character.Pickup == null)
			{
				character.Pickup = new List<ItemEntity>();
			}
			if(character.Pickup.Count >= maxNumber)
			{
				return result;
			}
			var itemPicks = items.Take(maxNumber - character.Pickup.Count);
			foreach (var item in itemPicks)
			{
				var entity = item.ToEntity();
				character.Pickup.Add(entity);
			}
			await Repostory.Update<OfflineCharacter>(actorId, b =>
			{
				b.Pickup = character.Pickup.DistinctBy(b=>b.Id).ToList();
			});
			result.SetSuccess(true);
			return result;
		}

		public async Task<IEnumerable<ItemEntity>> GetAllTemporaryItems(Guid? actorId)
		{
			if (await IsOnleService())
			{
				throw new NotImplementedException();
			}

			var character = await Repostory.GetCurrentOfflineCharacter(actorId);
			if (character == null) 
			{
				return Enumerable.Empty<ItemEntity>();
			}
			var result = character.Pickup ?? Enumerable.Empty<ItemEntity>();
			foreach(var r in result)
			{
				r.IsTempItem = true;
			}
			return result;
		}

		public async Task<IEnumerable<ItemEntity>> PickAndRemoveTemporaryItems(Guid? actorId, params Guid[] itemId)
		{
			if (await IsOnleService())
			{
				throw new NotImplementedException();
			}
			var character = await Repostory.GetCurrentOfflineCharacter(actorId);
			if (character == null)
			{
				return Enumerable.Empty<ItemEntity>();
			}
			if (character?.Pickup == null)
			{
				character.Pickup = new List<ItemEntity>();
			}
			var result = character.Pickup.Where(b => itemId.Contains(b.Id)).ToArray(); 
			character.Pickup = character?.Pickup?.Where(b => itemId.Contains(b.Id) != true).ToList();
			await Repostory.Update<OfflineCharacter>(actorId,b=> b.Pickup= character.Pickup);
			return result;
		}
	}
}
