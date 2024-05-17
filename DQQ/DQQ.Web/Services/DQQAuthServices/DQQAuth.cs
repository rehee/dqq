using BootstrapBlazor.Components;
using Microsoft.JSInterop;
using ReheeCmf.Commons.DTOs;

namespace DQQ.Web.Services.DQQAuthServices
{
  public class DQQAuth : IDQQAuth
  {
    private readonly ILocalStorageService localStorage;

    public DQQAuth(ILocalStorageService localStorage)
    {
      this.localStorage = localStorage;
    }
    public TokenDTO? GetAuth()
    {
      var token = localStorage.GetItem<TokenDTO>(nameof(TokenDTO));
      if (token == null)
      {
        localStorage.RemoveItem("localChar");
      }
      return token;
    }

    public bool IsAuth()
    {
      return GetAuth() != null;
    }

    public void SetAuth(TokenDTO? auth)
    {
      if (auth == null)
      {
        try
        {
          localStorage.RemoveItem(nameof(TokenDTO));
          localStorage.RemoveItem("localChar");
        }
        catch
        {

        }

      }
      else
      {
        localStorage.SetItem<TokenDTO?>(nameof(TokenDTO), auth);
      }
      
      return;
    }
  }
}
