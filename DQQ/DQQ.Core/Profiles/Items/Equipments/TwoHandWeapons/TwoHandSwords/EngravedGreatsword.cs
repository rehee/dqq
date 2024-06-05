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
  public class EngravedGreatsword : AbTwoHandSword
  {
    public override int DropQuantity => 1;
    public override decimal Rarity => 1m;
    public override EnumItem ProfileNumber => EnumItem.EngravedGreatsword;
    public override string? Name => "符文巨剑";
    public override string? Discription => "符文巨剑";
    public override decimal AttackPerSecond => 1.3m;
  }
}
