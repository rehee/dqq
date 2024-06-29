using DQQ.Web.Services.DQQAuthServices;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace DQQ.Web.Pages.Auths
{
  public partial class Logout
  {
    [Inject]
    [NotNull]
    public NavigationManager? nav { get; set; }

    [Inject]
    [NotNull]
    public IDQQAuth? auth { get; set; }

		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();
			await auth.SetAuth(null);
			nav.NavigateTo("", true);
		}

		
  }
}