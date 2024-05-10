
using DQQ.TickLogs;
using DQQ.Web.Services.Requests;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ReheeCmf.Commons.DTOs;
using ReheeCmf.Requests;
using System.Diagnostics.CodeAnalysis;

namespace DQQ.Web.Pages
{
  public partial class Home
  {
    [Inject]
    public ILocalStorageService? storage { get; set; }

    [Inject]
    [NotNull]
    public RequestClient<DQQGetHttpClient>? client { get; set; }

    protected override async Task OnInitializedAsync()
    {
      await base.OnInitializedAsync();
      var a = await client.Request<IEnumerable<TickLogItem>>(HttpMethod.Get, "my/map");
    }
  }
}