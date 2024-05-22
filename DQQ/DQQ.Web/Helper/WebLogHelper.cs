using BootstrapBlazor.Components;
using DQQ.TickLogs;


namespace DQQ.Helper
{
  public static class WebLogHelper
  {
    public static ConsoleMessageItem GetConsoleMessage(this TickLogItem item)
    {
      var result = new ConsoleMessageItem();
      Color color = Color.None;
      switch (item.LogType)
      {
        case Enums.EnumLogType.WaveChange:
          color = Color.Info;
          break;
        case Enums.EnumLogType.HealingTaken:
          color = Color.Success;
          break;
        default:
          if (item.From?.MobNumber != null)
          {
            color = Color.Danger;
          }
          if (item.From?.MobNumber == null)
          {
            color = Color.Active;
          }
          break;
      }
      var time = Math.Round(item.ActionSecond ?? 0m, 2);
      result.Color = color;

      result.Message = $"{time} {item.GetTextMessage()}";
      return result;
    }
    public static string GetTextMessage(this TickLogItem item)
    {
      switch (item.LogType)
      {
        case Enums.EnumLogType.WaveChange:
          return $"新波次 {item.WaveNumber}";
        case Enums.EnumLogType.CastSkill:
          return $"波次 ({item.WaveNumber}) {item.From?.DisplayName} 对 {item.Target?.DisplayName} 释放了 {item.Skill?.SkillName}";
        case Enums.EnumLogType.DamageTaken:
          return $"波次 ({item.WaveNumber}) {item.From?.DisplayName} 对 {item.Target?.DisplayName} 造成了 {item.Damage?.DamagePoint} 伤害 {(item.Damage?.IsKilled == true ? "击杀" : "")}";
        case Enums.EnumLogType.HealingTaken:
          return $"波次 ({item.WaveNumber}) {item.From?.DisplayName} 对 {item.Target?.DisplayName} 治疗了 {item.Healing?.HealingDone} 其中 {item.Healing?.OverHeal} 过量治疗";
      }

      return "";
    }
  }
}
