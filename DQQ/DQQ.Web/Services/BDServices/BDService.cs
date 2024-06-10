using Blazor.Serialization.Extensions;
using DQQ.Commons.DTOs;
using DQQ.Services.BDServices;
using DQQ.Web.Services.Requests;
using ReheeCmf.Requests;
using ReheeCmf.Responses;

namespace DQQ.Web.Services.BDServices
{
	public class BDService : ClientServiceBase, IBDService
	{
		public BDService(RequestClient<DQQGetHttpClient> client) : base(client)
		{
		}

		public async Task<ContentResponse<bool>> ApplyBuild(BuildDTO dto)
		{
			return await client.Request<bool>(HttpMethod.Post, "Build/Apply", dto.ToJson());
		}

		public async Task<ContentResponse<bool>> CreateNewBuild(BuildDTO dto)
		{
			return await client.Request<bool>(HttpMethod.Post, "Build", dto.ToJson());
		}

		public async Task<ContentResponse<bool>> DeleteBuild(BuildDTO dto)
		{
			return await client.Request<bool>(HttpMethod.Delete, "Build", dto.ToJson());
		}

		public async Task<IEnumerable<BuildSummaryDTO>?> GetAllBuild(Guid? actorId)
		{
			return (await client.Request<IEnumerable<BuildSummaryDTO>?>(HttpMethod.Get, $"Build/{actorId}")).Content;
		}

		public async Task<ContentResponse<bool>> UpdateBuild(BuildDTO dto)
		{
			return await client.Request<bool>(HttpMethod.Put, "Build", dto.ToJson());
		}
	}
}
