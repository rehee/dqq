using DQQ.Combats;
using DQQ.Commons;
using DQQ.Components.Stages;
using DQQ.Components.Stages.Actors;
using DQQ.Components.Stages.Maps;
using DQQ.Enums;
using DQQ.Pools;
using DQQ.Profiles.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Helper
{
  public static class DamageHelper
  {
    public static Int64 BasicDamage(this EnumDamageHand? hand, ITarget? caster, IMap? map)
    {
      if (caster == null)
      {
        return 0;
      }
      var property = caster!.CombatPanel.DynamicPanel;
      var baseDamage = property.Damage ?? 0;
      if (caster is IActor actor)
      {
        baseDamage = baseDamage + actor.BasicDamage;
      }
      var mainHandDamage = property.MainHand == null ? 0 : property.MainHand.Value + baseDamage;
      var offHandDamage = property.OffHand == null ? 0 : property.OffHand.Value + baseDamage;
      if (hand == null)
      {
        return property.MainHand == null ? baseDamage : mainHandDamage;
      }
      switch (hand)
      {
        case EnumDamageHand.Any:
          return property.MainHand == null ? baseDamage : mainHandDamage;
        case EnumDamageHand.MainHand:
          return mainHandDamage == 0 ? baseDamage : mainHandDamage;
        case EnumDamageHand.OffHand:
          return offHandDamage;
        case EnumDamageHand.BothHand:
          return (mainHandDamage + offHandDamage) == 0 ? baseDamage : mainHandDamage + offHandDamage;
      }
      return baseDamage;
    }
    public static DamageDeal[] SkillDamage(this ISkill skill, ITarget caster, IMap? map)
    {
      var profile = DQQPool.TryGet<SkillProfile, EnumSkill?>(skill.SkillNumber);
      if (profile == null || skill.DamageRate == 0)
      {
        return Enumerable.Empty<DamageDeal>().ToArray();
      }
      var damageHand = profile.DamageHand;
      if (profile.DamageHand == EnumDamageHand.EachHand)
      {
        if (caster.PrevioursMainHand == false || caster.PrevioursMainHand == null)
        {
          damageHand = EnumDamageHand.MainHand;
        }
        else
        {
          damageHand = EnumDamageHand.OffHand;
        }
      }
      var basicDamage = DamageHelper.BasicDamage(damageHand, caster, map);
      if (basicDamage == 0)
      {
        return Enumerable.Empty<DamageDeal>().ToArray();
      }
      var skillDamage = basicDamage.Percentage(profile.DamageRate);
      return [DamageDeal.New(skillDamage)];
    }

    public static Int64 SkillMordifier(this long baseDamage, ITarget? caster)
    {
      var modifier = caster?.CombatPanel.DynamicPanel.DamageModifier ?? 0;
      if (modifier == 0)
      {
        return baseDamage;
      }
      var p = baseDamage.Percentage(modifier);
      return baseDamage + p;
    }
    public static DamageDeal[] SkillMordifier(this DamageDeal[] damageDeals, ITarget? caster)
    {
      return damageDeals.Select(b => b.SkillMordifier(caster)).ToArray();
    }
    public static DamageDeal SkillMordifier(this DamageDeal damageDeals, ITarget? caster)
    {
      var modifier = caster?.CombatPanel.DynamicPanel.DamageModifier ?? 0;
      if (modifier == 0)
      {
        return DamageDeal.New(damageDeals.DamagePoint, damageDeals.DamageType);
      }
      var p = damageDeals.DamagePoint.Percentage(modifier + 1);
      return DamageDeal.New(p, damageDeals.DamageType);
    }
    public static Int64 Percentage(this Int64? input, decimal percentage)
    {
      if (input == null)
      {
        return 0;
      }
      return input.Value.Percentage(percentage);
    }
    public static Int64 Percentage(this Int64 input, decimal percentage, int times = 100)
    {

      var multiple = (int)(percentage * times);
      return input * multiple / times;
    }
  }
}
