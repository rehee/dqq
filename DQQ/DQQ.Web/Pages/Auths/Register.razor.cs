using Blazor.Serialization.Extensions;
using DQQ.Commons.DTOs;
using DQQ.Web.Services.DQQAuthServices;
using Microsoft.AspNetCore.Components;
using ReheeCmf.Commons.DTOs;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace DQQ.Web.Pages.Auths
{
  public partial class Register
  {
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? ConfirmPassword { get; set; }
    [Inject]
    [NotNull]
    public IDQQAuth? auth { get; set; }

    [Inject]
    [NotNull]
    public HttpClient? Http { get; set; }
    [Inject]
    [NotNull]
    public NavigationManager? nav { get; set; }
    public async Task RegisterMethod()
    {
      auth.SetAuth(null);
      if (Password != ConfirmPassword)
      {
        return;
      }

      var register = await Http.PostAsJsonAsync<RegisterDTO>(
        "Auths/Register",
        new RegisterDTO
        {
          Email = Email,
          Password = Password,
          ConfirmPassword = ConfirmPassword
        }
        , CancellationToken.None);
      if (!register.IsSuccessStatusCode)
      {
        return;
      }

      var result = await Http.PostAsJsonAsync<LoginDTO>(
        "Api/Token/Login",
        new LoginDTO { Username = Email, Password = Password, KeepLogin = true }, CancellationToken.None);
      if (!result.IsSuccessStatusCode)
      {
        auth.SetAuth(null);
        return;
      }
      try
      {
        var token = (await result.Content.ReadAsStringAsync()).FromJson<TokenDTO>();
        auth.SetAuth(token);
        nav.NavigateTo("", true);
      }
      catch
      {
        auth.SetAuth(null);
      }

    }
  }
}