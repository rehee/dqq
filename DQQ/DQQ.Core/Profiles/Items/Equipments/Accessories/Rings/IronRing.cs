using DQQ.Attributes;
using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Items.Equipments.Accessories.Rings
{
  [Pooled]
  public class IronRing : AbRing
  {
    public override decimal Rarity => 1;

    public override EnumItem ProfileNumber => EnumItem.IronRing;

    public override string? Name => "铁戒指";

    public override string? Discription => "铁戒指";
  }
}
