﻿using DQQ.Enums;
using DQQ.Web.Pages.DQQs;

namespace DQQ.Helper
{
  public static class StrageyHelper
  {
    public static string? GetStragyEnumString<T>(this T? input)
    {
      if (input is EnumStrategyCondition esc)
      {
        return GetStragyEnumStringType(esc);
      }
      if (input is EnumTarget et)
      {
        return GetStragyEnumStringType(et);
      }
      if (input is EnumTargetPriority etp)
      {
        return GetStragyEnumStringType(etp);
      }
      if (input is EnumPropertyCompare epc)
      {
        return GetStragyEnumStringType(epc);
      }
      if (input is EnumCompare ec)
      {
        return GetStragyEnumStringType(ec);
      }
      if (input is EnumStrategyParty esp)
      {
        return GetStragyEnumStringType(esp);
      }
      if (input is EnumStrategyWave esw)
      {
        return GetStragyEnumStringType(esw);
      }
      return null;
    }
    public static string? GetStragyEnumStringType(EnumStrategyCondition? input)
    {
      switch (input)
      {
        case EnumStrategyCondition.Target:
          return "单一目标";
        case EnumStrategyCondition.Enemies:
          return "敌方目标";
        case EnumStrategyCondition.Players:
          return "友方目标";
        case EnumStrategyCondition.Combat:
          return "当前战斗";
        case EnumStrategyCondition.Wave:
          return "当前波次";
        default:
          return "无策略";
      }
    }
    public static string? GetStragyEnumStringType(EnumTarget? input)
    {
      switch (input)
      {
        case EnumTarget.Target:
          return "敌人";
        case EnumTarget.Self:
          return "自身";
        default:
          return "无策略";
      }
    }
    public static string? GetStragyEnumStringType(EnumTargetPriority? input)
    {
      switch (input)
      {
        case EnumTargetPriority.AnyTarget:
          return "任意目标";
        case EnumTargetPriority.Strongest:
          return "最强目标";
        case EnumTargetPriority.Weakest:
          return "最弱目标";
        case EnumTargetPriority.Front:
          return "队列前方";
        case EnumTargetPriority.Back:
          return "队列后方";
        default:
          return "无策略";
      }
    }
    public static string? GetStragyEnumStringType(EnumPropertyCompare? input)
    {
      switch (input)
      {
        case EnumPropertyCompare.HealthPercentage:
          return "生命百分比";
        case EnumPropertyCompare.HealthAmount:
          return "生命绝对值";
        default:
          return "无策略";
      }
    }
    public static string? GetStragyEnumStringType(EnumCompare? input)
    {
      switch (input)
      {
        case EnumCompare.MoreThan: return "大于";
        case EnumCompare.MoreOrEqual: return "大于等于";
        case EnumCompare.LessThan: return "小于";
        case EnumCompare.LessOrEqual: return "小于等于";
        case EnumCompare.Equal: return "等于";
        case EnumCompare.NotEqual: return "不等于";
        default:
          return "无策略";
      }
    }
    public static string? GetStragyEnumStringType(EnumStrategyParty? input)
    {
      switch (input)
      {
        case EnumStrategyParty.AliveNumber: return "存活数量";
        case EnumStrategyParty.Contain: return "包含条件";

        default:
          return "无策略";
      }
    }
    public static string? GetStragyEnumStringType(EnumStrategyWave? input)
    {
      switch (input)
      {
        case EnumStrategyWave.OnlyBeginning: return "仅在开始时";
        case EnumStrategyWave.Period: return "周期释放";
        case EnumStrategyWave.BossFight: return "仅首领战";
        case EnumStrategyWave.EliteFight: return "仅精英战";
        case EnumStrategyWave.TrashMob: return "仅小怪战";

        default:
          return "无策略";
      }
    }


  }
}