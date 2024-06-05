using DQQ.Attributes;
using DQQ.Enums;
using DQQ.Profiles.Items.Equipments.Armors.ArmorTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Items.Equipments.Armors.BodyArmor
{
  [Pooled]
  public class ScaleArmour : AbBodyArmor<ArmorTypeArmor>
  {
    public override decimal Rarity => 1;

    public override EnumItem ProfileNumber => EnumItem.ScaleArmour;

    public override string? Name => "鳞甲盔甲";

    public override string? Discription => "鳞甲盔甲";
  }
}
