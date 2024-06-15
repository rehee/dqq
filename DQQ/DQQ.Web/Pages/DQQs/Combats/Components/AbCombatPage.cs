

using DQQ.Commons.DTOs;
using DQQ.Services.CombatServices;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace DQQ.Web.Pages.DQQs.Combats.Components
{
  public abstract class AbCombatPage : DQQPageBase
  {

    [Parameter]
    public Guid? ActorId { get; set; }

    [Inject]
    [NotNull]
    public ICombatService? combatService { get; set; }

    public CombatResultDTO? Result { get; set; }
    protected override async Task OnInitializedAsync()
    {
      await base.OnInitializedAsync();
      var result = await combatService.RequestCombatRandom(new Commons.DTOs.CombatRequestDTO
      {
        ActorId = ActorId,

      });
      Result = result?.Content;
    }
    protected override async Task OnDisposeAsync()
    {
      await base.OnDisposeAsync();
    }
  }
}
