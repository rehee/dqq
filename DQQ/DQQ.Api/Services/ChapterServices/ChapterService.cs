using DQQ.Entities;
using DQQ.Enums;
using DQQ.Helper;
using DQQ.Services.ActorServices;
using DQQ.Services.ChapterServices;
using Microsoft.EntityFrameworkCore;
using ReheeCmf.Contexts;
using ReheeCmf.Helpers;
using ReheeCmf.Responses;

namespace DQQ.Api.Services.ChapterServices
{
	public class ChapterService : IChapterService
	{
		private readonly IContext context;
		private readonly ICharacterService characterService;

		public ChapterService(IContext context, ICharacterService characterService)
		{
			this.context = context;
			this.characterService = characterService;
		}
		public async Task<ContentResponse<bool>> ProcessChapter(Guid? actorId, EnumChapter? chapter = null)
		{
			var result = new ContentResponse<bool>();
			var actor = await characterService.GetCharacter(actorId);
			if (actor == null)
			{
				return result;
			}
			var actorEntity = await context.Query<ActorEntity>(false).Where(b => b.Id == actorId).FirstOrDefaultAsync();
			if (actorEntity == null)
			{
				return result;
			}
			if (chapter != null)
			{
				actorEntity!.Chapter = chapter!.Value;
			}
			else
			{
				actorEntity!.Chapter = actor.NextChapter();
			}
			await context.SaveChangesAsync();
			result.SetSuccess(true);
			return result;
		}
	}
}
