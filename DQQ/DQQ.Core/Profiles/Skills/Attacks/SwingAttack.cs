using DQQ.Attributes;
using DQQ.Components.Stages;
using DQQ.Components.Stages.Actors;
using DQQ.Components.Stages.Actors.Characters;
using DQQ.Components.Stages.Maps;
using DQQ.Enums;
using DQQ.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Skills.Attacks
{
  [Pooled]
  public class SwingAttack : GeneralSkill
  {
    public override decimal CastTime => 0m;
    public override decimal CoolDown => 6m;
    public override decimal DamageRate => 1.5m;
    public override string? Discription => "迅捷的一击.造成武器伤害150%的伤害. 双持时候同时造成主副手伤害并且有额外加成.";
    public override string? Name => "迅猛攻击";
    public override bool CastWithWeaponSpeed => false;

    public override EnumSkill ProfileNumber => EnumSkill.SwingAttack;



    public override Int64 CalculateDamage(ITarget? caster, IMap? map)
    {
      var skill = this;
      if (caster is not IActor)
      {
        return 0;
      }
      if (caster is Character character)
      {
        character.Equips.TryGetValue(EnumEquipSlot.MainHand, out var main);
        character.Equips.TryGetValue(EnumEquipSlot.MainHand, out var off);
        if (main != null && off != null)
        {
          var total = character.MainHand + character.OffHand;

          return (total ?? 0).Percentage(skill.DamageRate) * 2;
        }

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


  }
}