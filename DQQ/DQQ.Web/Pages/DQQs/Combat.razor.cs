using BootstrapBlazor.Components;
using DQQ.Services.ActorServices;
using DQQ.Services.CombatServices;
using DQQ.TickLogs;
using DQQ.Web.Services.Requests;
using Microsoft.AspNetCore.Components;
using ReheeCmf.Requests;
using System.Diagnostics.CodeAnalysis;

namespace DQQ.Web.Pages.DQQs
{
  public class CombatPage : DQQPageBase
  {
    [Inject]
    [NotNull]
    public ICharacterService? characterService { get; set; }

    [Inject]
    [NotNull]
    public ICombatService? combatService { get; set; }

    public IEnumerable<TickLogItem>? CombatLog { get; set; }

    protected override async Task OnInitializedAsync()
    {
      await base.OnInitializedAsync();
      var actorId = characterService.GetSelectedCharacter();
      if (actorId == null)
      {
        nav.NavigateTo("");
        return;
      }
      var result = await combatService.RequestCombat(new Commons.DTOs.CombatRequestDTO
      {
        ActorId = actorId,
      });
      CombatLog = result.Content?.Logs;
      StateHasChanged();
    }

  }
}