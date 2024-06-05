using DQQ.Attributes;
using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Items.Equipments.Accessories.Amulets
{
  [Pooled]
  public class AmberAmulet : AbAmulet
  {
    public override decimal Rarity => 1;

    public override EnumItem ProfileNumber => EnumItem.AmberAmulet;

    public override string? Name => "琥珀护身符";

    public override string? Discription => "琥珀护身符";
  }
}
