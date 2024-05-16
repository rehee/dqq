using BootstrapBlazor.Components;
using DQQ.Web.Services.DQQAuthServices;
using DQQ.Web.Services.Requests;
using Microsoft.AspNetCore.Components;
using ReheeCmf.Requests;
using System.Diagnostics.CodeAnalysis;

namespace DQQ.Web.Layout
{
  public class MainLayoutPage : LayoutComponentBase
  {
    [Inject]
    [NotNull]
    public IGetRequestTokenService? tokenService { get; set; }

    [Inject]
    [NotNull]
    public IDQQAuth? auth { get; set; }

    [Inject]
    [NotNull]
    public NavigationManager? nav { get; set; }

    protected override async Task OnInitializedAsync()
    {
      await base.OnInitializedAsync();
      var isAuth = auth.IsAuth();
      if (isAuth)
      {
        if (tokenService is DQQGetRequestTokenService dqq)
        {
          var result = await dqq.CheckRequestTokenAsync();
          if (string.IsNullOrEmpty(result.token))
          {
            nav.NavigateTo("", true);
            return;
          }
        }
      }


    }
  }
}
