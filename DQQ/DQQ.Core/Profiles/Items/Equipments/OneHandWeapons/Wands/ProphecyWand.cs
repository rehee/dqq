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
  public class ProphecyWand : AbWand
  {
    public override decimal AttackPerSecond => 1.2m;

    public override decimal Rarity => 1;

    public override EnumItem ProfileNumber => EnumItem.ProphecyWand;

    public override string? Name => "灵兆法杖";

    public override string? Discription => "灵兆法杖";
  }
}
