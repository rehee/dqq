using DQQ.Enums;
using DQQ.Services.StrategyServices;
using Microsoft.AspNetCore.Mvc;

namespace DQQ.Api.Apis
{
  [ApiController]
  [Route("Strategy")]
  public class StrategyController : Controller
  {
    private readonly IStrategyService strategryService;

    public StrategyController(IStrategyService strategryService)
    {
      this.strategryService = strategryService;
    }
    [HttpPut("Target/{actorId}/{priority?}")]
    public async Task<bool> SetTargetPriority(Guid? actorId, EnumTargetPriority? priority)
    {
      var result = await strategryService.SetActorTargetPriority(actorId, priority);
      return result.Success;
    }
  }
}
