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
  public class LeatherGlove : AbGlove<ArmorTypeDefence>
  {
    public override decimal Rarity => 1;

    public override EnumItem ProfileNumber => EnumItem.LeatherGlove;

    public override string? Name => "皮甲护手";

    public override string? Discription => "皮甲护手";
  }
}
