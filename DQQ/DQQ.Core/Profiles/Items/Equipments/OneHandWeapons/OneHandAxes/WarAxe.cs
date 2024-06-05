using DQQ.Attributes;
using DQQ.Enums;
using DQQ.Profiles.Items.Equipments.OneHandWeapons.Daggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Items.Equipments.OneHandWeapons.OneHandAxes
{
  [Pooled]
  public class WarAxe : AbOneHandAxe
  {
    public override decimal AttackPerSecond => 1.3m;

    public override decimal Rarity => 1;

    public override EnumItem ProfileNumber => EnumItem.WarAxe;

    public override string? Name => "行军斧";

    public override string? Discription => "行军斧";
  }
}
