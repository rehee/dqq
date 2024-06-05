using DQQ.Attributes;
using DQQ.Enums;
using DQQ.Profiles.Items.Equipments.OneHandWeapons.Daggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Items.Equipments.OneHandWeapons.OneHandMaces
{
  [Pooled]
  public class PetrifiedClub : AbOneHandMace
  {
    public override decimal AttackPerSecond => 1.25m;

    public override decimal Rarity => 1;

    public override EnumItem ProfileNumber => EnumItem.PetrifiedClub;

    public override string? Name => "坚石木棒";

    public override string? Discription => "坚石木棒";
  }
}
