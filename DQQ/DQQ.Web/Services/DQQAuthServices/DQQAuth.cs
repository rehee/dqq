using DQQ.Services;
using ReheeCmf.Commons.DTOs;

namespace DQQ.Web.Services.DQQAuthServices
{
	public class DQQAuth : IDQQAuth
  {
    private readonly IGameStatusService gameStatusService;

    public DQQAuth(IGameStatusService gameStatusService)
    {
      this.gameStatusService = gameStatusService;
    }
    public async Task<TokenDTO?> GetAuth()
    {
      var status = await gameStatusService.GetOrCreateGameStatus();
      var token = status?.Content?.Token;
			if (token == null && status?.Success==true)
      {
        status!.Content!.CurrentCharId = null;
        await gameStatusService.UpdateGameStatus(status?.Content);
			}
			return token;
    }

    public async Task<bool> IsAuth()
    {
      return (await GetAuth()) != null;
    }

    public async Task SetAuth(TokenDTO? auth)
    {
			var status = await gameStatusService.GetOrCreateGameStatus();
      if (status.Success != true)
      {
        return;
      }
			if (auth == null)
      {
        try
        {
          status!.Content = null;
					
        }
        catch
        {

        }

      }
      else
      {
				status!.Content!.Token = auth;
        status!.Content!.OwnerId = auth?.UserId;
			}
      await gameStatusService.UpdateGameStatus(status?.Content);
			return;
    }
  }
}
