
using DQQ.Web.Pages;

namespace DQQ.Web.Layout
{
  public class NavMenuPage : DQQPageBase
  {
    public bool collapseNavMenu = true;

    public string? NavMenuCssClass => collapseNavMenu ? "collapse" : null;

    public void ToggleNavMenu()
    {
      collapseNavMenu = !collapseNavMenu;
    }
    protected override Task OnInitializedAsync()
    {
      return base.OnInitializedAsync();
    }

    public async Task OpenReadMe()
    {

      await dialogService.ShowComponent<Readme>(null, "About DQQ");
    }
  }
}