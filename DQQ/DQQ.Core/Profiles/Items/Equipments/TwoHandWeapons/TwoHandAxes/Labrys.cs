using DQQ.Attributes;
using DQQ.Enums;
using DQQ.Profiles.Items.Equipments.TwoHandWeapons.Bows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Items.Equipments.TwoHandWeapons.TwoHandAxes
{
  [Pooled]
  public class Labrys : AbTwoHandAxe
  {
    public override decimal AttackPerSecond => 1.2m;

    public override decimal Rarity => 1;

    public override EnumItem ProfileNumber => EnumItem.Labrys;

    public override string? Name => "双影巨斧";

    public override string? Discription => "双影巨斧";
  }
}
