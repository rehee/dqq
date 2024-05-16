
using DQQ.Commons.DTOs;
using DQQ.Services.ActorServices;
using DQQ.Services.CombatServices;
using Microsoft.AspNetCore.Components;
using ReheeCmf.Responses;
using System.Diagnostics.CodeAnalysis;

namespace DQQ.Web.Pages.DQQs.Combats
{
  public class CombatSummaryPage : DQQPageBase
  {

    public CombatResultDTO? Result { get; set; }
    public bool IsCombat = false;

    protected override async Task OnInitializedAsync()
    {
      await base.OnInitializedAsync();
    }

    public async Task CombatRequest()
    {
      await dialogService.ShowComponent<CombatRequest>(null, null, true, async (f) =>
      {
        if (f.ResultValue is ContentResponse<CombatResultDTO?> rv)
        {
          Result = rv.Content;
        }
        StateHasChanged();
      });
    }
    public async Task CombatLog()
    {
      await dialogService.ShowComponent<CombatLog>(
        new Dictionary<string, object?>
        {
          ["CombatLog"] = Result?.Logs
        }
        , null, false);
    }
  }
}