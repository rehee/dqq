using DQQ.Services;
using DQQ.Web.Datas;
using DQQ.Web.Services.Requests;
using ReheeCmf.Requests;
using System.Diagnostics.CodeAnalysis;

namespace DQQ.Web.Services
{
  public abstract class ClientServiceBase
  {
    [NotNull]
    protected readonly RequestClient<DQQGetHttpClient>? client;
    protected readonly IIndexRepostory Repostory;
		protected readonly IGameStatusService StatusService;


		protected ClientServiceBase(RequestClient<DQQGetHttpClient> client, IIndexRepostory repostory, IGameStatusService statusService)
    {
      this.client = client;
      Repostory = repostory;
      StatusService = statusService;

		}

    public async Task<bool> IsOnleService()
    {
      return (await StatusService.GetOrCreateGameStatus()).Content?.PlayMode != Enums.EnumPlayMode.Offline;
		}
  }
}
