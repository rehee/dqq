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
  public class RustedHatchet : AbOneHandAxe
  {
    public override decimal AttackPerSecond => 1.5m;

    public override decimal Rarity => 1;

    public override EnumItem ProfileNumber => EnumItem.RustedHatchet;

    public override string? Name => "锈斧";

    public override string? Discription => "锈斧";
  }
}
