using DQQ.Combats;
using DQQ.Components.Stages.Maps;
using DQQ.Enums;
using DQQ.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Affixes
{
  public class AffixPower
  {
    public EnumPropertyType PropertyType { get; set; }
    public int? Power { get; set; }

    public void SetProperty(ICombatProperty? property)
    {
      if (property == null)
      {
        return;
      }
      if (Power == null)
      {
        return;
      }
      var percentage = Power.DefaultValue(0) / 100m;
      switch (PropertyType)
      {
        case EnumPropertyType.MaximunLife:
          property.MaximunLife = property.MaximunLife.DefaultValue() + Power;
          break;
        case EnumPropertyType.Armor:
          property.Armor = property.Armor.DefaultValue() + Power;
          break;
        case EnumPropertyType.Damage:
          property.Damage = property.Damage.DefaultValue() + Power;
          break;
        case EnumPropertyType.MainHand:
          property.MainHand = property.MainHand.DefaultValue() + Power;
          break;
        case EnumPropertyType.OffHand:
          property.OffHand = property.OffHand.DefaultValue() + Power;
          break;

        case EnumPropertyType.AttackRating:
          property.AttackRating = property.AttackRating.DefaultValue() + Power;
          break;
        case EnumPropertyType.Defence:
          property.Defence = property.Defence.DefaultValue() + Power;
          break;
        case EnumPropertyType.DefencePercentage:
          property.DefencePercentage = property.DefencePercentage.DefaultValue() + percentage;
          break;
        case EnumPropertyType.BlockChance:
          property.BlockChance = property.BlockChance.DefaultValue() + percentage;
          break;
        case EnumPropertyType.BlockRecovery:
          property.BlockRecovery = property.BlockRecovery.DefaultValue() + percentage;
          break;
        case EnumPropertyType.DodgeChance:
          property.DodgeChance = property.DodgeChance.DefaultValue() + percentage;
          break;
        case EnumPropertyType.PhysicsResistance:
          property.PhysicsResistance = property.PhysicsResistance.DefaultValue() + Power;
          break;
        case EnumPropertyType.FireResistance:
          property.FireResistance = property.FireResistance.DefaultValue() + Power;
          break;
        case EnumPropertyType.ColdResistance:
          property.ColdResistance = property.ColdResistance.DefaultValue() + Power;
          break;
        case EnumPropertyType.LightningResistance:
          property.LightningResistance = property.LightningResistance.DefaultValue() + Power;
          break;
        case EnumPropertyType.ChaosResistance:
          property.ChaosResistance = property.ChaosResistance.DefaultValue() + Power;
          break;
        case EnumPropertyType.PhysicsDamageModifier:
          property.PhysicsDamageModifier = property.PhysicsDamageModifier.DefaultValue() + percentage;
          break;
        case EnumPropertyType.FireDamageModifier:
          property.FireDamageModifier = property.FireDamageModifier.DefaultValue() + percentage;
          break;
        case EnumPropertyType.ColdDamageModifier:
          property.ColdDamageModifier = property.ColdDamageModifier.DefaultValue() + percentage;
          break;
        case EnumPropertyType.LightningDamageModifier:
          property.LightningDamageModifier = property.LightningDamageModifier.DefaultValue() + percentage;
          break;
        case EnumPropertyType.ChaosDamageModifier:
          property.ChaosDamageModifier = property.ChaosDamageModifier.DefaultValue() + percentage;
          break;


        case EnumPropertyType.AttackPerSecond:
          property.AttackPerSecond = property.AttackPerSecond.DefaultValue() + percentage;
          break;
        case EnumPropertyType.Resistance:
          property.Resistance = property.Resistance.DefaultValue() + percentage;
          break;
        case EnumPropertyType.DamageModifier:
          property.DamageModifier = property.DamageModifier.DefaultValue() + percentage;
          break;
      }
    }
  }
}
