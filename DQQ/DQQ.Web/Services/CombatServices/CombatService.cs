using Blazor.Serialization.Extensions;
using DQQ.Commons.DTOs;
using DQQ.Services.CombatServices;
using DQQ.Web.Services.Requests;
using ReheeCmf.Requests;
using ReheeCmf.Responses;

namespace DQQ.Web.Services.CombatServices
{
  public class CombatService : ClientServiceBase, ICombatService
  {
    public CombatService(RequestClient<DQQGetHttpClient> client) : base(client)
    {
    }

    public async Task<ContentResponse<CombatResultDTO>> RequestCombat(CombatRequestDTO dto)
    {
      return await client.Request<CombatResultDTO>(HttpMethod.Post, "Combat/Request", dto.ToJson());
    }
  }
}
