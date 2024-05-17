using Blazor.Serialization.Extensions;
using DQQ.Enums;
using DQQ.Services.StrategyServices;
using DQQ.Strategies.SkillStrategies;
using DQQ.Web.Services.Requests;
using ReheeCmf.Requests;
using ReheeCmf.Responses;

namespace DQQ.Web.Services.StrategyServices
{
  public class StrategyService : ClientServiceBase, IStrategyService
  {
    public StrategyService(RequestClient<DQQGetHttpClient> client) : base(client)
    {
    }

    public async Task<ContentResponse<bool>> SetActorSkillStrategy(Guid? actorId, int slot, IEnumerable<SkillStrategy>? strategies)
    {
      return await client.Request<bool>(HttpMethod.Put, $"Strategy/SkillStrategy/{actorId}/{slot}", strategies.ToJson());
    }

    public async Task<ContentResponse<bool>> SetActorTargetPriority(Guid? actorId, EnumTargetPriority? priority)
    {
      return await client.Request<bool>(HttpMethod.Put, $"Strategy/Target/{actorId}/{priority}");
    }
  }
}
