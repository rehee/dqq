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
      var percentage = Power / 100;
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
