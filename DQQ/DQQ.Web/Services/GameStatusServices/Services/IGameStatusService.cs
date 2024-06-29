using DQQ.Entities;
using ReheeCmf.Responses;

namespace DQQ.Services
{
	public interface IGameStatusService
	{
		Task<ContentResponse<GameStatus>> GetOrCreateGameStatus();
		Task<ContentResponse<bool>> UpdateGameStatus(GameStatus? status);
		
	}
}
