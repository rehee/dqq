using DQQ.Attributes;
using DQQ.Enums;
using DQQ.Profiles.Items.Equipments.Armors.ArmorTypes;
using DQQ.Profiles.Items.Equipments.Armors.BodyArmor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Items.Equipments.Armors.Boots
{
  [Pooled]
  public class PlateBoot : AbBoots<ArmorTypeArmor>
  {
    public override decimal Rarity => 1;

    public override EnumItem ProfileNumber => EnumItem.PlateBoot;

    public override string? Name => "板甲靴";

    public override string? Discription => "板甲靴";
  }
}
