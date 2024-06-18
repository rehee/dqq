using DQQ.Enums;
using DQQ.Services.ChapterServices;
using DQQ.Web.Services.Requests;
using ReheeCmf.Requests;
using ReheeCmf.Responses;

namespace DQQ.Web.Services.ChapterServices
{
	public class ChapterService : ClientServiceBase, IChapterService
	{
		public ChapterService(RequestClient<DQQGetHttpClient> client) : base(client)
		{
		}

		public async Task<ContentResponse<bool>> ProcessChapter(Guid? actorId, EnumChapter? chapter = null)
		{
			return await client.Request<bool>(HttpMethod.Post, $"Chapter/Next/{actorId}");
		}
	}
}
