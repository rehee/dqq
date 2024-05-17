using DQQ.Enums;
using DQQ.Services.StrategyServices;
using Microsoft.AspNetCore.Mvc;
using ReheeCmf.Authenticates;

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
    [CmfAuthorize(AuthOnly = true)]
    public async Task<bool> SetTargetPriority(Guid? actorId, EnumTargetPriority? priority)
    {
      Console.WriteLine(actorId);
      Console.WriteLine(priority);
      var result = await strategryService.SetActorTargetPriority(actorId, priority);
      return result.Success;
    }
  }
}
