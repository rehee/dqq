using DQQ.Attributes;
using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Items.Equipments.TwoHandWeapons.Bows
{
  [Pooled]
  public class ShortBow : AbBow
  {
    public override decimal AttackPerSecond => 1.5m;

    public override decimal Rarity => 1;

    public override EnumItem ProfileNumber => EnumItem.ShortBow;

    public override string? Name => "短弓";

    public override string? Discription => "短弓";
  }
}
