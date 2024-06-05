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
  public class PlateArmor : AbBodyArmor<ArmorTypeArmor>
  {
    public override decimal Rarity => 1;

    public override EnumItem ProfileNumber => EnumItem.PlateArmor;

    public override string? Name => "板甲";

    public override string? Discription => "板甲";
  }
}
