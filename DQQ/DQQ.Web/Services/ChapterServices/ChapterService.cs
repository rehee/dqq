using DQQ.Entities;
using DQQ.Enums;
using DQQ.Services;
using DQQ.Services.ActorServices;
using DQQ.Services.ChapterServices;
using DQQ.Web.Datas;
using DQQ.Web.Resources.Chapters.C_0;
using DQQ.Web.Services.Requests;
using ReheeCmf.Helpers;
using ReheeCmf.Requests;
using ReheeCmf.Responses;

namespace DQQ.Web.Services.ChapterServices
{
	public class ChapterService : ClientServiceBase, IChapterService
	{
		private readonly ICharacterService characterService;

		public ChapterService(ICharacterService characterService, RequestClient<DQQGetHttpClient> client, IIndexRepostory repostory, IGameStatusService statusService) : base(client, repostory, statusService)
		{
			this.characterService = characterService;
		}

		public async Task<ContentResponse<bool>> ProcessChapter(Guid? actorId, EnumChapter? chapter = null)
		{
			if(await IsOnleService())
			{
				return await client.Request<bool>(HttpMethod.Post, $"Chapter/Next/{actorId}");
			}
			else
			{
				var result = new ContentResponse<bool>();
				var offline = (await Repostory.Read<OfflineCharacter>()).FirstOrDefault(b=>b.Id == actorId);
				
				await Repostory.Update<OfflineCharacter>(actorId, (a) => { a.SelectedCharacter.Chapter = chapter ?? EnumChapter.None; });
				result.SetSuccess(true);
				return result;
			}
			
		}
	}
}
