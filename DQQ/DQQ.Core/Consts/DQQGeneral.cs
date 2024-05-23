using DQQ.Components.Stages.Maps;
using DQQ.Enums;
using DQQ.Helper;
using System.Numerics;

namespace DQQ.Consts
{
  public static class DQQGeneral
  {
    public const int TickPerSecond = 30;
    public const int MapMinute = 30;
    public const int DelayTime = 10;
    public const decimal MinCooldown = 0.15m;

    public const int MinimunMapLevel = 50;
    public const int LevelPerTier = 3;
    public const int LevelPerSubTier = 1;
    public const decimal MobLevelIncreased = 0.05m;

    public const int DurationIntervalTick = 15;

    public const decimal DuelwieldAttackSpeed = 1.10m;
    public static EnumTargetLevel[] GuardianLevel = new EnumTargetLevel[] { EnumTargetLevel.Guardian };
    public static EnumTargetLevel[] EliteLevel = new EnumTargetLevel[] { EnumTargetLevel.Magic, EnumTargetLevel.Elite, EnumTargetLevel.Champion };
    public static EnumTargetLevel[] TrashLevel = new EnumTargetLevel[] { EnumTargetLevel.NotSpecified, EnumTargetLevel.Normal };
    public static Int64 MobStatusCalculate(int mobLevel, Int64 basicValue, EnumMobRarity? rarity = EnumMobRarity.Normal, bool isBoss = false)
    {
      var multiple = 1;
      if (isBoss != true)
      {
        switch (rarity)
        {
          case EnumMobRarity.Magic:
            multiple = 2;
            break;
          case EnumMobRarity.Elite:
            multiple = 3;
            break;
          case EnumMobRarity.Champion:
            multiple = 5;
            break;
        }
      }
      else
      {
        multiple = 2;
      }
      if (mobLevel <= 1)
      {
        return basicValue;
      }
      var value = (mobLevel - 1) * MobLevelIncreased;
      return (basicValue + basicValue.Percentage(value)) * multiple;
    }
    public static int TotalTick(this IMap? map) => 60 * ((map?.limitMinute ?? MapMinute) * TickPerSecond);
  }
}
