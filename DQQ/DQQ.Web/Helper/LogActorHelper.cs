using BootstrapBlazor.Components;
using DQQ.Enums;
using DQQ.TickLogs;
using System.ComponentModel;
using System.Reflection.PortableExecutable;

namespace DQQ.Helper
{
  public static class LogActorHelper
  {
    public static Color GetTargetPowerLevelColor(this TickLogActor? actor,Color defaultColor = Color.None )
    {
      switch (actor?.PowerLevel)
      {
        case EnumTargetLevel.NotSpecified:
        case EnumTargetLevel.Normal:
          return defaultColor;
        case EnumTargetLevel.Magic:
          return Color.Primary;
        case EnumTargetLevel.Elite:
          return Color.Info;
        case EnumTargetLevel.Champion:
          return Color.Warning;
        case EnumTargetLevel.Guardian:
          return Color.Danger;
      }
      return defaultColor;
    }
  }
}
