using DQQ.Attributes;
using DQQ.Components.Items;
using DQQ.Components.Items.Equips;
using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Items.Equipments.TwoHandWeapons
{
  [Pooled]
  public class CorrodedBlade : AbTwoHandWeapon
  {
    public override int DropQuantity => 1;
    public override decimal Rarity => 1m;
    public override EnumItem ProfileNumber => EnumItem.CorrodedBlade;
    public override string? Name => "双手剑";
    public override string? Discription => "双手剑";

    public override decimal AttackPerSecond => 1m;
    public override Int64 BaseDamage => 10;

    
  }
}
