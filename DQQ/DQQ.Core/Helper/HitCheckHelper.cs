using DQQ.Commons;
using DQQ.Components.Parameters;
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
    public static EnumHitCheck HitCheck(BeforeDamageTakenParameter parameter, SkillHitCheck? skillCheck)
    {
      if (skillCheck?.HitCheck == EnumHitCheck.Hit)
      {
        return EnumHitCheck.Hit;
      }
      if (parameter.To == null)
      {
        return EnumHitCheck.Hit;
      }
      if (MissCheck(parameter, skillCheck))
      {
        return EnumHitCheck.Miss;
      }

      if (DodgeCheck(parameter, skillCheck))
      {
        return EnumHitCheck.Dodge;
      }
      if (BlockCheck(parameter, skillCheck))
      {
        return EnumHitCheck.Block;
      }
      return EnumHitCheck.Hit;
    }

    public static bool BlockCheck(BeforeDamageTakenParameter parameter, SkillHitCheck? skillCheck)
    {
      if (skillCheck?.IgnoreCheck?.Any(b => b == EnumHitCheck.Block) == true)
      {
        return false;
      }
      var blockChance = (parameter?.To?.CombatPanel.DynamicPanel.BlockChance).DefaultValue();
      if (blockChance <= 0)
      {
        return false;
      }
      if (blockChance < RandomHelper.GetRandom(0))
      {
        return false;
      }
      return parameter?.To?.TryBlock().Success == true;
    }
    public static bool DodgeCheck(BeforeDamageTakenParameter parameter, SkillHitCheck? skillCheck)
    {
      if (skillCheck?.IgnoreCheck?.Any(b => b == EnumHitCheck.Dodge) == true)
      {
        return false;
      }
      var dogeChance = (parameter?.To?.CombatPanel.DynamicPanel.DodgeChance).DefaultValue();
      if (dogeChance <= 0)
      {
        return false;
      }

      return dogeChance >= RandomHelper.GetRandom(0);
    }
    public static bool MissCheck(BeforeDamageTakenParameter parameter, SkillHitCheck? skillCheck)
    {
      var levelDifferent = (parameter?.From?.Level).DefaultValue() - (parameter?.To?.Level).DefaultValue();
      var baseHitChance = DQQGeneral.SameLevelHitChance + levelDifferent * DQQGeneral.HitChanceModifyByLevel;

      long attributeDifference =
        ((parameter?.From?.CombatPanel.DynamicPanel.AttackRating).DefaultValue() + (parameter?.From?.Level).DefaultValue(1)) -
        ((parameter?.To?.CombatPanel.DynamicPanel.Defence).DefaultValue() + (parameter?.To?.Level).DefaultValue(1));
      baseHitChance = baseHitChance + DQQGeneral.AttributeImpact * attributeDifference;
      baseHitChance = Math.Max(DQQGeneral.MinHitChance, baseHitChance);
      baseHitChance = Math.Min(DQQGeneral.MaxHitChance, baseHitChance);
      RandomHelper.GetRandom(0);

      return baseHitChance <= RandomHelper.GetRandom(0);
    }
  }
}
