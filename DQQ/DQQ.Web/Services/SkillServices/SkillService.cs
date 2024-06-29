using DQQ.Commons.DTOs;
using DQQ.Pools;
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

		public async Task<IEnumerable<SkillDTO>> GetAllSkills()
		{
			await Task.CompletedTask;
			return DQQPool.SkillPool.Select(b => b.Value).PlayerAvaliableSkill().Select(b => new SkillDTO { SkillNumber = b.SkillNumber }).ToArray();
		}

		public async Task<ContentResponse<bool>> PickSkill(PickSkillDTO dto)
		{
			var result = await client.Request<bool>(HttpMethod.Post, "Skills", dto.ToJson());
			return result;
		}

		public Task<ContentResponse<bool>> PickSkills(Guid? actorId, params PickSkillDTO?[] dtos)
		{
			throw new NotImplementedException();
		}
	}
}
