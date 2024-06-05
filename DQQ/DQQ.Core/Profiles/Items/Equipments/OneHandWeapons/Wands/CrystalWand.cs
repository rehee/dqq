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
  public class CrystalWand : AbWand
  {
    public override decimal AttackPerSecond => 1.3m;

    public override decimal Rarity => 1;

    public override EnumItem ProfileNumber => EnumItem.CrystalWand;

    public override string? Name => "水晶法杖";

    public override string? Discription => "水晶法杖";
  }
}
