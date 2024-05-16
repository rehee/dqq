using Blazor.Serialization.Extensions;
using DQQ.Web.Services.DQQAuthServices;
using ReheeCmf.Commons.DTOs;
using ReheeCmf.Requests;
using System.Net.Http.Json;

namespace DQQ.Web.Services.Requests
{
  public class DQQGetRequestTokenService : IGetRequestTokenService
  {
    private readonly IDQQAuth auth;
    private readonly HttpClient http;

    public DQQGetRequestTokenService(IDQQAuth auth, HttpClient http)
    {
      this.auth = auth;
      this.http = http;
    }
    public async Task<(string? name, string? token)> GetRequestTokenAsync(CancellationToken ct = default)
    {
      var token = auth.GetAuth();
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
      auth.SetAuth(newTokenDTO);
      return (null, newTokenDTO.TokenString);
    }
    public async Task<(string? name, string? token)> CheckRequestTokenAsync(CancellationToken ct = default)
    {
      var token = auth.GetAuth();
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
        auth.SetAuth(null);
        return (null, null);
      }
      var newTokenDTO = (await newTokenResponse.Content.ReadAsStringAsync()).FromJson<TokenDTO>();
      if (newTokenDTO == null)
      {
        auth.SetAuth(null);
        return (null, null);
      }
      auth.SetAuth(newTokenDTO);
      return (null, newTokenDTO.TokenString);
    }
  }
}
