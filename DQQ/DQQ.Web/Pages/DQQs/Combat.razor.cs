using DQQ.TickLogs;
using DQQ.Web.Services.Requests;
using Microsoft.AspNetCore.Components;
using ReheeCmf.Requests;
using System.Diagnostics.CodeAnalysis;

namespace DQQ.Web.Pages.DQQs
{
  public partial class Combat
  {
    [Inject]
    [NotNull]
    public RequestClient<DQQGetHttpClient>? client { get; set; }

    public IEnumerable<TickLogItem>? CombatLog { get; set; }

    protected override async Task OnInitializedAsync()
    {
      await base.OnInitializedAsync();
      var result = await client.Request<IEnumerable<TickLogItem>>(HttpMethod.Get, "my/map");
      CombatLog = result.Content;
      StateHasChanged();
    }

  }
}