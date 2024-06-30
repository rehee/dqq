using DQQ.Entities;
using DQQ.Services;
using DQQ.Web.Datas;
using DQQ.Web.Enums;

namespace DQQ.Helper
{
	public static class OfflineCharacterHelp
	{
		public static async Task<OfflineCharacter?> GetCurrentOfflineCharacter(
			this IIndexRepostory repostory, Guid? actorId)
		{
			var characters = await repostory.Read<OfflineCharacter>();
			return characters.FirstOrDefault(b=> b.Id == actorId);
		}
	}
}
