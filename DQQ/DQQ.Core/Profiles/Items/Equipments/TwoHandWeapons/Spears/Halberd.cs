using DQQ.Attributes;
using DQQ.Enums;
using DQQ.Profiles.Items.Equipments.TwoHandWeapons.Bows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Items.Equipments.TwoHandWeapons.Spears
{
  [Pooled]
  public class Halberd : AbSpear
  {
    public override decimal AttackPerSecond => 1.1m;

    public override decimal Rarity => 1;

    public override EnumItem ProfileNumber => EnumItem.Halberd;

    public override string? Name => "戟";

    public override string? Discription => "戟";
  }
}
