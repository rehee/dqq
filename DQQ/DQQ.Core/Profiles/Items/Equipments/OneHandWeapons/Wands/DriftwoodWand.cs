using DQQ.Attributes;
using DQQ.Enums;
using DQQ.Profiles.Items.Equipments.OneHandWeapons.Scepters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Items.Equipments.OneHandWeapons.Wands
{
  [Pooled]
  public class DriftwoodWand : AbWand
  {
    public override decimal AttackPerSecond => 1.4m;

    public override decimal Rarity => 1;

    public override EnumItem ProfileNumber => EnumItem.DriftwoodWand;

    public override string? Name => "朽木法杖";

    public override string? Discription => "朽木法杖";
  }
}
