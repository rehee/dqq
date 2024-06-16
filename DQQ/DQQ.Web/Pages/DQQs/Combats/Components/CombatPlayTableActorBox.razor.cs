using BootstrapBlazor.Components;
using DQQ.TickLogs;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace DQQ.Web.Pages.DQQs.Combats.Components
{
  public partial class CombatPlayTableActorBox
  {

    [Parameter]
    [NotNull]
    public TickLogActor? Actor { get; set; }

    [Parameter]
    public TickLogItem? Item { get; set; }

    public bool IsMob => Actor?.MobNumber != null;

    public Color ThisColor => IsMob ? Actor.GetTargetPowerLevelColor() : Color.None;
  }
}