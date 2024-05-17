
using DQQ.Services.ActorServices;
using DQQ.Services.CombatServices;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace DQQ.Web.Pages.DQQs.Combats
{
  public class CombatRequestPage : DQQPageBase
  {
    public int MapLevel { get; set; }
    public int SubMapLevel { get; set; }

    

    [Inject]
    [NotNull]
    public ICombatService? combatService { get; set; }

    [Parameter]
    public Guid? ActorId { get; set; }
    public override async Task<bool> SaveFunction()
    {
      var result = await combatService.RequestCombat(new Commons.DTOs.CombatRequestDTO
      {
        ActorId = ActorId,
        MapLevel = MapLevel,
        SubMapLevel = SubMapLevel
      });
      this.OnSave!.ResultValue = result;

      return true;
    }
  }
}