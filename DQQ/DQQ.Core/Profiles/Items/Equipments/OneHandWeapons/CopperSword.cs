﻿using DQQ.Attributes;
using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Items.Equipments.OneHandWeapons
{
  [Pooled]
  public class CopperSword : AbOneHandWeapon
  {
    public override decimal AttackPerSecond => 1.5m;

    public override long BaseDamage => 5;

    public override int DropQuantity => 1;

    public override decimal Rarity => 1m;

    public override EnumItem ProfileNumber => EnumItem.CopperSword;

    public override string? Name => "单手剑";

    public override string? Discription => "单手剑";
  }
}
