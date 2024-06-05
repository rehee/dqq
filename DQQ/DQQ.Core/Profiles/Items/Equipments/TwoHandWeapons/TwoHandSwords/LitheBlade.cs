using DQQ.Attributes;
using DQQ.Components.Items;
using DQQ.Components.Items.Equips;
using DQQ.Enums;
using DQQ.Profiles.Items.Equipments.TwoHandWeapons.Bows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Items.Equipments.TwoHandWeapons.TwoHandSwords
{
  [Pooled]
  public class LitheBlade : AbTwoHandSword
  {
    public override int DropQuantity => 1;
    public override decimal Rarity => 1m;
    public override EnumItem ProfileNumber => EnumItem.LitheBlade;
    public override string? Name => "细刃";
    public override string? Discription => "细刃";
    public override decimal AttackPerSecond => 1.35m;
  }
}
