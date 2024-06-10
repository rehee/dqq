using DQQ.Commons.DTOs;
using DQQ.Services.BDServices;
using Microsoft.AspNetCore.Mvc;
using ReheeCmf.Authenticates;
using ReheeCmf.Modules.Controllers;
using ReheeCmf.Responses;

namespace DQQ.Api.Apis
{
	[ApiController]
	[Route("Build")]
	public class BuildController : ReheeCmfController
	{
		private readonly IBDService service;

		public BuildController(IServiceProvider sp, IBDService service) : base(sp)
		{
			this.service = service;
		}

		[CmfAuthorize(AuthOnly = true)]
		[HttpPost("Apply")]
		public async Task<bool> ApplyBuild(BuildDTO dto)
		{
			return (await service.ApplyBuild(dto)).Success;
		}

		[CmfAuthorize(AuthOnly = true)]
		[HttpPost]
		public async Task<bool> CreateNewBuild(BuildDTO dto)
		{
			return (await service.CreateNewBuild(dto)).Success;
		}

		[CmfAuthorize(AuthOnly = true)]
		[HttpDelete]
		public async Task<bool> DeleteBuild(BuildDTO dto)
		{
			return (await service.DeleteBuild(dto)).Success;
		}

		[CmfAuthorize(AuthOnly = true)]
		[HttpGet("{actorId}")]
		public async Task<IEnumerable<BuildSummaryDTO>?> GetAllBuild(Guid? actorId)
		{
			return await service.GetAllBuild(actorId);
		}

		[CmfAuthorize(AuthOnly = true)]
		[HttpPut]
		public async Task<bool> UpdateBuild(BuildDTO dto)
		{
			return (await service.UpdateBuild(dto)).Success;
		}
	}
}
