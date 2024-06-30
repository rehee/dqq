using DQQ.Enums;
using DQQ.Services.StrategyServices;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace DQQ.Web.Pages.DQQs.Strategies
{
  public class TargetPriorityPage : DQQPageBase
  {
    [Inject]
    [NotNull]
    public IStrategyService? strageService { get; set; }

    [Parameter]
    public EnumTargetPriority? TargetPriority { get; set; }
   

    public async Task SelectPriority(EnumTargetPriority? targetPriority)
    {
      TargetPriority = targetPriority;
    }

    public override async Task<bool> SaveFunction()
    {
      var result = await strageService.SetActorTargetPriority(ActorId, TargetPriority);
			ParentRefreshEvent.InvokeEvent(this, EventArgs.Empty);
			return result.Success;
    }
  }
}