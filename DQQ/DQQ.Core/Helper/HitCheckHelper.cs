using DQQ.Commons;
using DQQ.Components.Stages;
using DQQ.Components.Stages.Maps;
using DQQ.Consts;
using DQQ.Enums;
using DQQ.Profiles;
using DQQ.Profiles.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Helper
{
  public static class HitCheckHelper
  {
    public static EnumHitCheck HitCheck(this ITarget? from, ITarget? to, IMap? map, SkillHitCheck? skillCheck)
    {
      if (skillCheck?.HitCheck == EnumHitCheck.Hit)
      {
        return EnumHitCheck.Hit;
      }
      if (to == null)
      {
        return EnumHitCheck.Hit;
      }
      if (MissCheck(from, to, map, skillCheck))
      {
        return EnumHitCheck.Miss;
      }

      if (DodgeCheck(to, map, skillCheck))
      {
        return EnumHitCheck.Dodge;
      }
      if (BlockCheck(to, map, skillCheck))
      {
        return EnumHitCheck.Block;
      }
      return EnumHitCheck.Hit;
    }

    public static bool BlockCheck(ITarget? to, IMap map, SkillHitCheck? skillCheck)
    {
      if (skillCheck?.IgnoreCheck?.Any(b => b == EnumHitCheck.Block) == true)
      {
        return false;
      }
      var blockChance = (to?.CombatPanel.DynamicPanel.BlockChance).DefaultValue();
      if (blockChance <= 0)
      {
        return false;
      }
      if (blockChance < RandomHelper.GetRandom(0))
      {
        return false;
      }
      return to?.TryBlock().Success == true;
    }
    public static bool DodgeCheck(ITarget? to, IMap map, SkillHitCheck? skillCheck)
    {
      if (skillCheck?.IgnoreCheck?.Any(b => b == EnumHitCheck.Dodge) == true)
      {
        return false;
      }
      var dogeChance = (to?.CombatPanel.DynamicPanel.DodgeChance).DefaultValue();
      if (dogeChance <= 0)
      {
        return false;
      }

      return dogeChance >= RandomHelper.GetRandom(0);
    }
    public static bool MissCheck(ITarget? from, ITarget? to, IMap map, SkillHitCheck? skillCheck)
    {
      var levelDifferent = (from?.Level).DefaultValue() - (to?.Level).DefaultValue();
      var baseHitChance = DQQGeneral.SameLevelHitChance + levelDifferent * DQQGeneral.HitChanceModifyByLevel;

      long attributeDifference =
        ((from?.CombatPanel.DynamicPanel.AttackRating).DefaultValue() + (from?.Level).DefaultValue(1)) -
        ((to?.CombatPanel.DynamicPanel.Defence).DefaultValue() + (to?.Level).DefaultValue(1));
      baseHitChance = baseHitChance + DQQGeneral.AttributeImpact * attributeDifference;
      baseHitChance = Math.Max(DQQGeneral.MinHitChance, baseHitChance);
      baseHitChance = Math.Min(DQQGeneral.MaxHitChance, baseHitChance);
      RandomHelper.GetRandom(0);

      return baseHitChance <= RandomHelper.GetRandom(0);
    }
  }
}
