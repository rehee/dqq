using DQQ.Components.Stages;
using DQQ.Components.Stages.Actors;
using DQQ.Components.Stages.Maps;
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
    public static Int64 SkillDamage(ISkill skill, ITarget caster, IMap? map)
    {
      if (caster is not IActor)
      {
        return 0;
      }
      var actor = caster as IActor;
      Int64 damage = 0;
      if (caster.MainHand == null)
      {
        damage = actor!.BasicDamage.Percentage(skill.DamageRate);
      }
      else
      {
        if (caster.OffHand == null)
        {
          damage = actor!.MainHand!.Value.Percentage(skill.DamageRate);
        }
        else
        {
          if (caster.PrevioursMainHand == null)
          {
            caster.PrevioursMainHand = true;
            damage = actor!.MainHand!.Value.Percentage(skill.DamageRate);
          }
          else
          {
            if (caster.PrevioursMainHand == true)
            {
              damage = actor!.OffHand!.Value.Percentage(skill.DamageRate);
            }
            else
            {
              damage = actor!.MainHand!.Value.Percentage(skill.DamageRate);
            }
            caster.PrevioursMainHand = !caster.PrevioursMainHand;
          }
        }
      }

      return damage;
    }

    public static Int64 Percentage(this Int64? input, decimal percentage)
    {
      if (input == null)
      {
        return 0;
      }
      return input.Value.Percentage(percentage);
    }
    public static Int64 Percentage(this Int64 input, decimal percentage)
    {
      var times = 100;
      var multiple = (int)(percentage * times);
      return input * multiple / times;
    }
  }
}
