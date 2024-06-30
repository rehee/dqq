using DQQ.Commons.DTOs;
using DQQ.Entities;
using DQQ.Enums;
using DQQ.Services;
using DQQ.Services.SkillServices;
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
		private readonly ISkillService skillService;

		public StrategyService(ISkillService skillService, RequestClient<DQQGetHttpClient> client, IIndexRepostory repostory, IGameStatusService statusService) : base(client, repostory, statusService)
		{
			this.skillService = skillService;
		}

		public async Task<ContentResponse<bool>> SetActorSkillStrategy(Guid? actorId, EnumSkillSlot slot, IEnumerable<SkillStrategyDTO>? strategies)
		{
			if(await IsOnleService())
			{
				return await client.Request<bool>(HttpMethod.Put, $"Strategy/SkillStrategy/{actorId}/{slot}", strategies?.ToJson());
			}
			var character = await Repostory.GetCurrentOfflineCharacter(actorId);

			if(character?.SelectedCharacter?.SkillMap?.TryGetValue(slot, out var skill) == true)
			{
				skill.SkillStrategies = strategies?.ToList() ?? [];
				var pick = PickSkillDTO.New(skill, actorId, slot);
				return await skillService.PickSkill(pick);
			}

			return new ContentResponse<bool>();
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
