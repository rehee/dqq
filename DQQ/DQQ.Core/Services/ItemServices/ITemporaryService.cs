using DQQ.Components.Items;
using DQQ.Entities;
using ReheeCmf.Responses;

namespace DQQ.Api.Services.Itemservices
{
  public interface ITemporaryService
  {
    Task<IEnumerable<ItemEntity>> GetAllTemporaryItems(Guid actorId);
    Task<IEnumerable<ItemEntity>> PickAndRemoveTemporaryItems(Guid actorId, params Guid[] itemId);
    Task<ContentResponse<bool>> AddAndIntoTemporary(Guid actorId, params ItemComponent[] items);
  }
}
