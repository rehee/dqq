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
    public static BigInteger SkillDamage(ISkill skill, ITarget caster, IMap? map)
    {
      if (caster is not IActor)
      {
        return 0;
      }
      var actor = caster as IActor;
      BigInteger damage = 0;
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

    public static BigInteger Percentage(this BigInteger input, decimal percentage)
    {
      var times = 100;
      var multiple = (int)(percentage * times);
      return input * multiple / times;
    }
  }
}
