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
          return "col-md-6";
        }
        switch (Actor.PowerLevel)
        {
          case Enums.EnumTargetLevel.Guardian:
            return "col-md-6";
          case Enums.EnumTargetLevel.Champion:
          case Enums.EnumTargetLevel.Elite:
          case Enums.EnumTargetLevel.Magic:
            return "col-sm-6 col-md-3";
          default:
            return "col-sm-3 col-md-3 col-lg-2";
        }

      }
    }

  }
}