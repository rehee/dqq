using DQQ.Profiles.Affixes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Items.Equipments.Armors.ArmorTypes
{
  public interface IArmorType
  {
    AffixeRange[]? Ranges { get; }
  }

  public static class ArmorTypeExtend
  {
    public static ArmorTypeArmor Armor { get; set; } = new ArmorTypeArmor();
    public static ArmorTypeDefence Defence { get; set; } = new ArmorTypeDefence();
    public static ArmorTypeArmorDefence ArmorDefence { get; set; } = new ArmorTypeArmorDefence();


  }

}
