using DQQ.Components.Items;
using DQQ.Entities;
using ReheeCmf.Responses;

namespace DQQ.Api.Services.Itemservices
{
  public interface ITemporaryService
  {
    Task<IEnumerable<ItemEntity>> GetAllTemporaryItems(Guid actorId);
    Task<ItemEntity?> GetTemporaryItems(Guid actorId, Guid itemId);
    Task<ContentResponse<bool>> InsertIntoTemporary(Guid actorId, params ItemComponent[] items);
  }
}
