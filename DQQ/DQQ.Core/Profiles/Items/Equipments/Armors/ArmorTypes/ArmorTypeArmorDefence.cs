using DQQ.Enums;
using DQQ.Profiles.Affixes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Items.Equipments.Armors.ArmorTypes
{
  public class ArmorTypeArmorDefence : IArmorType
  {
    public AffixeRange[]? Ranges => [
      AffixeRange.New(EnumPropertyType.Armor,5,1500, EnumAffixeRangeType.LevelBased,21m),
      AffixeRange.New(EnumPropertyType.Defence,5,1500, EnumAffixeRangeType.LevelBased,21m)
      ];
  }
}
