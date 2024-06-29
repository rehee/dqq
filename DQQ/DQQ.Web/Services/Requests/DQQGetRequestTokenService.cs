using DQQ.Services;
using DQQ.Web.Services.DQQAuthServices;
using ReheeCmf.Commons.DTOs;
using ReheeCmf.Requests;
using System.Net.Http.Json;

namespace DQQ.Web.Services.Requests
{
	public class DQQGetRequestTokenService : IGetRequestTokenService
  {
		private readonly IGameStatusService gameStatusService;
		private readonly IDQQAuth auth;
    private readonly HttpClient http;

    public DQQGetRequestTokenService(IGameStatusService gameStatusService, IDQQAuth auth, HttpClient http)
    {
			this.gameStatusService = gameStatusService;
			this.auth = auth;
      this.http = http;
    }
    public async Task<(string? name, string? token)> GetRequestTokenAsync(CancellationToken ct = default)
    {
      var isOffline = await CheckOfflineToken();
      if (isOffline != null)
      {
        return isOffline!.Value;
      }

			var token = await auth.GetAuth();
      if (token == null)
      {
        return (null, null);
      }
      var expiretime = token.ExpireUCTTime;
      if (expiretime >= DateTime.UtcNow.AddMinutes(-1))
      {
        return ("", token.TokenString);
      }
      var newTokenResponse = await http.PostAsJsonAsync<TokenValidate>("Api/Token/RefreshAccessToken", new TokenValidate
      {
        Token = token.RefreshTokenString
      });

      if (!newTokenResponse.IsSuccessStatusCode)
      {
        return (null, null);
      }
      var newTokenDTO = (await newTokenResponse.Content.ReadAsStringAsync()).FromJson<TokenDTO>();
      if (newTokenDTO == null)
      {
        return (null, null);
      }
      await auth.SetAuth(newTokenDTO);
      return (null, newTokenDTO.TokenString);
    }
    public async Task<(string? name, string? token)> CheckRequestTokenAsync(CancellationToken ct = default)
    {
			var isOffline = await CheckOfflineToken();
			if (isOffline != null)
			{
				return isOffline!.Value;
			}
			var token = await auth.GetAuth();
      if (token == null)
      {
        return (null, null);
      }
      var newTokenResponse = await http.PostAsJsonAsync<TokenValidate>("Api/Token/RefreshAccessToken", new TokenValidate
      {
        Token = token.RefreshTokenString
      });

      if (!newTokenResponse.IsSuccessStatusCode)
      {
        await auth.SetAuth(null);
        return (null, null);
      }
      var newTokenDTO = (await newTokenResponse.Content.ReadAsStringAsync()).FromJson<TokenDTO>();
      if (newTokenDTO == null)
      {
        await auth.SetAuth(null);
        return (null, null);
      }
      await auth.SetAuth(newTokenDTO);
      return (newTokenDTO.UserName, newTokenDTO.TokenString);
    }

    public async Task<(string? name, string? token)?> CheckOfflineToken()
    {
      var status = await gameStatusService.GetOrCreateGameStatus();
      if(status?.Content?.PlayMode == Enums.EnumPlayMode.Offline && !String.IsNullOrEmpty(status?.Content?.OwnerId))
      {
        return ("", (new TokenDTO()).ToJson());
      }
      return null;

		}
	}
}
