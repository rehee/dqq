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
  public class DriftwoodClub : AbOneHandMace
  {
    public override decimal AttackPerSecond => 1.45m;

    public override decimal Rarity => 1;

    public override EnumItem ProfileNumber => EnumItem.DriftwoodClub;

    public override string? Name => "朽木之棒";

    public override string? Discription => "朽木之棒";
  }
}
