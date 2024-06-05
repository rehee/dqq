using DQQ.Attributes;
using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Items.Equipments.Accessories.Belts
{
  [Pooled]
  public class LeatherBelt : AbBelt
  {
    public override decimal Rarity => 1;

    public override EnumItem ProfileNumber => EnumItem.LeatherBelt;

    public override string? Name => "皮革腰带";

    public override string? Discription => "皮革腰带";
  }
}
