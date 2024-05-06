
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ReheeCmf.Commons.DTOs;

namespace DQQ.Web.Pages
{
  public partial class Home
  {
    [Inject]
    public ILocalStorageService? storage { get; set; }
    protected override async Task OnInitializedAsync()
    {
      await base.OnInitializedAsync();
      var b = storage!.GetItem<TokenDTO>("token");
      storage!.SetItem<TokenDTO>("token", new TokenDTO { UserId = "1" });
      var a = 1;
    }
  }
}