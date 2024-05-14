using Blazor.Serialization.Extensions;
using DQQ.Commons.DTOs;
using DQQ.Profiles.Skills;
using DQQ.Services.SkillServices;
using DQQ.Web.Services.Requests;
using ReheeCmf.Requests;
using ReheeCmf.Responses;

namespace DQQ.Web.Services.SkillServices
{
  public class SkillService : ClientServiceBase, ISkillService
  {
    public SkillService(RequestClient<DQQGetHttpClient> client) : base(client)
    {
    }

    public async Task<IEnumerable<ISkill>> GetAllSkills()
    {
      var result = await client.Request<IEnumerable<SkillDTO>>(HttpMethod.Get, "Skills");
      return result.Success ? result.Content! : Enumerable.Empty<ISkill>();
    }

    public async Task<ContentResponse<bool>> PickSkill(PickSkillDTO dto)
    {
      var result = await client.Request<bool>(HttpMethod.Post, "Skills", dto.ToJson());
      return result;
    }
  }
}
