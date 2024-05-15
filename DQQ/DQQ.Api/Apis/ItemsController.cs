using DQQ.Entities;
using DQQ.Services.ItemServices;
using Microsoft.AspNetCore.Mvc;
using ReheeCmf.Modules.Controllers;

namespace DQQ.Api.Apis
{
  [ApiController]
  [Route("Items")]
  public class ItemsController : ReheeCmfController
  {
    public ItemsController(IServiceProvider sp, IItemService itemService) : base(sp)
    {
      this.itemService = itemService;
    }

    private IItemService itemService { get; }

    [HttpGet("Pick/{actorId}")]
    public async Task<IEnumerable<ItemEntity>?> PickableItems(Guid? actorId)
    {
      if (actorId == null)
      {
        return Enumerable.Empty<ItemEntity>();
      }
      return await itemService.PickableItems(actorId.Value);
    }
    [HttpPost("Pick/{actorId}")]
    public async Task<bool> PickItems(Guid? actorId, Guid[]? items)
    {
      if (actorId == null || items?.Any() != true)
      {
        return false;
      }
      var result = await itemService.PickItem(actorId, items);
      return result.Success;
    }
  }
}
