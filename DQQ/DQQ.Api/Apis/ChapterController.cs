using DQQ.Commons.DTOs;
using DQQ.Services.ChapterServices;
using DQQ.Services.CombatServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReheeCmf.Authenticates;
using ReheeCmf.Modules.Controllers;

namespace DQQ.Api.Apis
{
	[Route("Chapter")]
	[ApiController]
	public class ChapterController : ReheeCmfController
	{
		private readonly IChapterService chapterService;

		public ChapterController(IServiceProvider sp, IChapterService chapterService) : base(sp)
		{
			this.chapterService = chapterService;
		}

		[HttpPost("Next/{actorId}")]
		[CmfAuthorize(AuthOnly = true)]
		public async Task<bool> NextChapter(Guid? actorId)
		{
			var result = await chapterService.ProcessChapter(actorId);
			return result.Success;
		}
	}
}
