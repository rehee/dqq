using DQQ.Entities;
using DQQ.Enums;
using DQQ.Services;
using DQQ.Services.StrategyServices;
using DQQ.Strategies.SkillStrategies;
using DQQ.Web.Datas;
using DQQ.Web.Services.Requests;
using ReheeCmf.Requests;
using ReheeCmf.Responses;

namespace DQQ.Web.Services.StrategyServices
{
	public class StrategyService : ClientServiceBase, IStrategyService
	{
		public StrategyService(RequestClient<DQQGetHttpClient> client, IIndexRepostory repostory, IGameStatusService statusService) : base(client, repostory, statusService)
		{
		}

		public async Task<ContentResponse<bool>> SetActorSkillStrategy(Guid? actorId, EnumSkillSlot slot, IEnumerable<SkillStrategy>? strategies)
		{
			return await client.Request<bool>(HttpMethod.Put, $"Strategy/SkillStrategy/{actorId}/{slot}", strategies?.ToJson());
		}

		public async Task<ContentResponse<bool>> SetActorTargetPriority(Guid? actorId, EnumTargetPriority? priority)
		{
			if (await IsOnleService())
			{
				return await client.Request<bool>(HttpMethod.Put, $"Strategy/Target/{actorId}/{priority}");
			}
			return await Repostory.Update<OfflineCharacter>(actorId, c =>
			{
				if (c == null|| c?.SelectedCharacter==null)
				{
					return;
				}
				c.SelectedCharacter.TargetPriority = priority;
			});
		}
	}
}
