using DQQ.Combats;
using DQQ.Enums;
using DQQ.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Affixes
{
  public class AffixeRange
  {
    public static AffixeRange New(EnumPropertyType PropertyType, int? Min = null, int? Max = null)
    {
      var result = new AffixeRange();
      result.PropertyType = PropertyType;

      result.Min = Min.GetValueOrDefault(1);
      result.Max = Max.GetValueOrDefault(result.Min) < result.Min ? result.Min : result.Max;

      return result;
    }
    public EnumPropertyType PropertyType { get; set; }
    public int Min { get; set; }
    public int Max { get; set; }
    public void SetProperty(ICombatProperty? property)
    {
      if (property == null)
      {
        return;
      }
      var getActualValue = RandomHelper.GetRandomInt(Min, Max);
      var percentage = getActualValue / 100;
      switch (PropertyType)
      {
        case EnumPropertyType.MaximunLife:
          property.MaximunLife = property.MaximunLife.DefaultValue() + getActualValue;
          break;
        case EnumPropertyType.Armor:
          property.Armor = property.Armor.DefaultValue() + getActualValue;
          break;
        case EnumPropertyType.Damage:
          property.Damage = property.Damage.DefaultValue() + getActualValue;
          break;
        case EnumPropertyType.MainHand:
          property.MainHand = property.MainHand.DefaultValue() + getActualValue;
          break;
        case EnumPropertyType.OffHand:
          property.OffHand = property.OffHand.DefaultValue() + getActualValue;
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
    public AffixPower NewPower()
    {
      return new AffixPower
      {
        PropertyType = PropertyType,
        Power = RandomHelper.GetRandomInt(Min, Max)
      };
    }
  }
}
