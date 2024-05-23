using BootstrapBlazor.Components;
using DQQ.Components.Stages.Actors;
using DQQ.TickLogs;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace DQQ.Web.Pages.DQQs.Combats
{
  public partial class ActorBox
  {
    [Parameter]
    [NotNull]
    public TickLogActor? Actor { get; set; }

    public bool IsMob => Actor?.MobNumber != null;

    public Color ThisColor => IsMob ? Actor.GetTargetPowerLevelColor() : Color.None;

    public string ColumnNumber
    {
      get
      {
        if (!IsMob)
        {
          return "col-12";
        }
        switch (Actor.PowerLevel)
        {
          case Enums.EnumTargetLevel.Guardian:
            return "col-6";
          case Enums.EnumTargetLevel.Champion:
          case Enums.EnumTargetLevel.Elite:
          case Enums.EnumTargetLevel.Magic:
            return "col-3";
          default:
            return "col-2";
        }

      }
    }

  }
}