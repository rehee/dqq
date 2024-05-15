using Blazor.Serialization.Extensions;
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
    public async Task LoginMethod()
    {

      var result = await Http.PostAsJsonAsync<Dictionary<string, string?>>(
        "Auths/Register",
        new Dictionary<string, string?> { 
          ["Username"] = Email,
          ["UserName"] = Email,
          ["Password"] = Password,
        }, CancellationToken.None);
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

      ;
    }
  }
}