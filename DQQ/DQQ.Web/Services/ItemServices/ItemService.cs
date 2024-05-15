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

    public Task<ContentResponse<bool>> EquipItem(Guid actorId, Guid itemId, EnumEquipSlot slot)
    {
      throw new NotImplementedException();
    }

    public async Task<IEnumerable<ItemEntity>?> PickableItems(Guid actorId)
    {
      return (await client.Request<IEnumerable<ItemEntity>?>(HttpMethod.Get, $"Items/Pick/{actorId}")).Content;


    }

    public async Task<ContentResponse<bool>> PickItem(Guid actorId, params Guid[] itemId)
    {
      throw new NotImplementedException();
    }

    public Task<ContentResponse<bool>> UnEquipItem(Guid actorId, params EnumEquipSlot[] slots)
    {
      throw new NotImplementedException();
    }
  }
}
