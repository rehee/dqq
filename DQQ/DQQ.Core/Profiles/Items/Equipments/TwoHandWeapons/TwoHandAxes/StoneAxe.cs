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
  public class StoneAxe : AbTwoHandAxe
  {
    public override decimal AttackPerSecond => 1.3m;

    public override decimal Rarity => 1;

    public override EnumItem ProfileNumber => EnumItem.StoneAxe;

    public override string? Name => "石斧";

    public override string? Discription => "石斧";
  }
}
