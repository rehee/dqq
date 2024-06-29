using DQQ.Web.Enums;
using DQQ.Web.Services.DQQAuthServices;
using Microsoft.AspNetCore.Components;
using ReheeCmf.Commons.DTOs;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Json;
using System.Text.Json;

namespace DQQ.Web.Pages.Auths
{
	public class LoginPage: AuthBasePage
	{
    public override EnumPlayMode PlayMode => EnumPlayMode.Online;
		public string? Email { get; set; }
    public string? Password { get; set; }

    [Inject]
    [NotNull]
    public IDQQAuth? auth { get; set; }

    [Inject]
    [NotNull]
    public HttpClient? Http { get; set; }
    [Inject]
    [NotNull]
    public NavigationManager? nav { get; set; }
		

		public async Task LoginMethod()
    {
      
      var result = await Http.PostAsJsonAsync<LoginDTO>(
        "Api/Token/Login",
        new LoginDTO { Username = Email, Password = Password, KeepLogin = true }, CancellationToken.None);
      if (!result.IsSuccessStatusCode)
      {
        await auth.SetAuth(null);
        return;
      }
      try
      {
        var token = JsonSerializer.Deserialize<TokenDTO>(await result.Content.ReadAsStringAsync());
				await auth.SetAuth(token);
        nav.NavigateTo("", true);
      }
      catch
      {
				await auth.SetAuth(null);
      }

      ;
    }
  }
}