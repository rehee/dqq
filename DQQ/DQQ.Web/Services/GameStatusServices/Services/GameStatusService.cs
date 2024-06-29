using DQQ.Entities;
using DQQ.Services;
using DQQ.Web.Datas;
using ReheeCmf.Helpers;
using ReheeCmf.Responses;

namespace DQQ.Web.Services.GameStatusServices.Services
{
	public class GameStatusService : IGameStatusService
	{
		private readonly IIndexRepostory repostory;

		public GameStatusService(IIndexRepostory repostory)
		{
			this.repostory = repostory;
		}
		public async Task<ContentResponse<GameStatus>> GetOrCreateGameStatus()
		{
			var response = new ContentResponse<GameStatus>();
			try
			{
				var result = await repostory.Read<GameStatus>();
				GameStatus enrity;
				if (result?.Any() == false)
				{
					enrity = new GameStatus();
					await repostory.Create(enrity);
				}
				else
				{
					enrity = result!.FirstOrDefault()!;
					var exture = result!.Where((b, i) => i > 0).ToArray();
					if (exture.Length > 0)
					{
						foreach (var e in exture)
						{
							await repostory.Delete(e);
						}
					}
				}
				response.SetSuccess(enrity);
			}
			catch (Exception ex) 
			{
				response.SetError(ex);
			}
			
			return response;
		}

		public async Task<ContentResponse<bool>> UpdateGameStatus(GameStatus? status)
		{
			var result = new ContentResponse<bool>();
			
			var existStatus = await GetOrCreateGameStatus();
			existStatus?.Content?.Set(status);
			await repostory.Update(existStatus?.Content);
			result.SetSuccess(true);
			return result;
		}
	}
}
