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
  public class RockBreaker : AbOneHandMace
  {
    public override decimal AttackPerSecond => 1.15m;

    public override decimal Rarity => 1;

    public override EnumItem ProfileNumber => EnumItem.RockBreaker;

    public override string? Name => "破岩锤";

    public override string? Discription => "破岩锤";
  }
}
