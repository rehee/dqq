using DQQ.Components.Items;
using DQQ.Components.Items.Equips;
using DQQ.Enums;
using DQQ.Helper;
using DQQ.Profiles.Affixes;
using DQQ.Profiles.Items.Equipments.Armors.ArmorTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Items.Equipments.Armors
{
  public abstract class AbArmor<T> : EquipProfile where T : class, IArmorType, new()
  {
    public abstract decimal PowerPercentage { get; }

    public override EquipComponent GenerateEquipComponent(int? itemLevel, EnumRarity rarity = EnumRarity.Normal)
    {
      var result = base.GenerateEquipComponent(itemLevel, rarity);

      var inputType = typeof(T);
      AffixeRange[]? ranges = null;
      if (inputType == typeof(ArmorTypeArmor))
      {
        ranges = ArmorTypeExtend.Armor.Ranges;
      }
      if (inputType == typeof(ArmorTypeDefence))
      {
        ranges = ArmorTypeExtend.Defence.Ranges;
      }
      if (inputType == typeof(ArmorTypeArmorDefence))
      {
        ranges = ArmorTypeExtend.ArmorDefence.Ranges;
      }
      if (ranges?.Any() == true)
      {
        foreach (var p in ranges.Select(b => b.NewPower(itemLevel.DefaultValue(1))))
        {
          p.Power = (int)Math.Round(p.Power.DefaultValue(0) * PowerPercentage, 0);
          p.SetProperty(result?.Property);
        }
      }

      return result!;
    }
  }
}
