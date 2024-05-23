using BootstrapBlazor.Components;
using DQQ.Commons;
using DQQ.Enums;
using DQQ.Pools;
using DQQ.TickLogs;

namespace DQQ.Helper
{
  public static class LogActorHelper
  {
    public static Color GetTargetPowerLevelColor(this TickLogActor? actor, Color defaultColor = Color.None)
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

    public static DurationIcon GetDurationIcon(this TickLogDuration duration)
    {
      var result = DurationIcon.New(Color.None, "");
      if (duration?.DurationNumber == null)
      {
        return result;
      }
      var profile = DQQPool.DurationPool[duration.DurationNumber];
      result.IconColor = profile.DurationType.GetDurationColor();
      result.IconName = profile.DurationType.GetDurationIcon();
      return result;
    }
    public static Color GetDurationColor(this EnumDurationType? durationType)
    {

      Color durationColor;

      switch (durationType)
      {
        case EnumDurationType.Buff:
          durationColor = Color.Warning;
          break;
        case EnumDurationType.Damage:
          durationColor = Color.Danger;
          break;
        case EnumDurationType.Healing:
          durationColor = Color.Success;
          break;
        default:
          durationColor = Color.None;
          break;
      }
      return durationColor;
    }
    public static string GetDurationIcon(this EnumDurationType? durationType)
    {
      string durationIcon;
      switch (durationType)
      {
        case EnumDurationType.Buff:
          durationIcon = "fa-solid fa-arrow-up-from-ground-water";
          break;
        case EnumDurationType.Damage:
          durationIcon = "fa-solid fa-droplet-slash";
          break;
        case EnumDurationType.Healing:
          durationIcon = "fa-solid fa-hand-holding-medical";
          break;
        default:
          durationIcon = "fa-solid fa-hourglass";
          break;
      }
      return durationIcon;
    }
  }
}
