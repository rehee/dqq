
using BootstrapBlazor.Components;
using DQQ.Commons.DTOs;
using DQQ.Components.Stages.Actors;
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

    [Inject]
    [NotNull]
    public ICombatService? combatService { get; set; }
    [Inject]
    [NotNull]
    private SwalService? SwalService { get; set; }
    protected override async Task OnInitializedAsync()
    {
      await base.OnInitializedAsync();
    }
    [Parameter]
    public Guid? ActorId { get; set; }
    public async Task CombatRequest()
    {
      await dialogService.ShowComponent<CombatRequest>(new Dictionary<string, object?>
      {
        ["ActorId"] = ActorId
      }, null, true, async (f) =>
      {
        if (f.ResultValue is ContentResponse<CombatResultDTO?> rv)
        {
          Result = rv.Content;
        }
        StateHasChanged();
      });
    }
    public async Task CombatRequest2()
    {
      var result = await combatService.RequestCombat(new Commons.DTOs.CombatRequestDTO
      {
        ActorId = ActorId,
        MapLevel = 0,
        SubMapLevel = 0
      });
      if (!result.Success)
      {
        var op = new SwalOption()
        {
          Category = SwalCategory.Error,
          Title = "Õ½¶·Ê§°Ü",
          Content = "Õ½¶·Ê§°Ü. ÇëÉÔµÈÆ¬¿Ì¼ÌÐø³¢ÊÔ",
          ShowClose = true
        };
        await SwalService.Show(op);
        return;
      }
      Result = result.Content;
      StateHasChanged();
      await dialogService.ShowComponent<CombatPlay>(
        new Dictionary<string, object?>
        {
          ["CombatLog"] = Result?.Logs
        }

        , null, false, async (f) =>
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