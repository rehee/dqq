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
  public class WristChopper : AbOneHandAxe
  {
    public override decimal AttackPerSecond => 1.2m;

    public override decimal Rarity => 1;

    public override EnumItem ProfileNumber => EnumItem.WristChopper;

    public override string? Name => "断腕之刃";

    public override string? Discription => "断腕之刃";
  }
}
