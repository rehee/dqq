using DQQ.Enums;
using DQQ.Services.StrategyServices;
using DQQ.Strategies.SkillStrategies;
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
			var result = await strategryService.SetActorTargetPriority(actorId, priority);
			return result.Success;
		}
		[HttpPut("SkillStrategy/{actorId}/{slot?}")]
		[CmfAuthorize(AuthOnly = true)]
		public async Task<bool> SetSkillStrategy(Guid? actorId, EnumSkillSlot slot, IEnumerable<SkillStrategy>? strategies)
		{
			var result = await strategryService.SetActorSkillStrategy(actorId, slot, strategies);
			return result.Success;
		}
	}
}
