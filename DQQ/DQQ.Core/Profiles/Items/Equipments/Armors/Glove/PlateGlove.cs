using DQQ.Attributes;
using DQQ.Enums;
using DQQ.Profiles.Items.Equipments.Armors.ArmorTypes;
using DQQ.Profiles.Items.Equipments.Armors.BodyArmor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Items.Equipments.Armors.Glove
{
  [Pooled]
  public class PlateGlove : AbGlove<ArmorTypeArmor>
  {
    public override decimal Rarity => 1;

    public override EnumItem ProfileNumber => EnumItem.PlateGlove;

    public override string? Name => "板甲护手";

    public override string? Discription => "板甲护手";
  }
}
