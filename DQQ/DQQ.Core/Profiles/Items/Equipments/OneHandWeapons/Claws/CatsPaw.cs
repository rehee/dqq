using DQQ.Attributes;
using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Items.Equipments.OneHandWeapons.Claws
{
  [Pooled]
  public class CatsPaw : AbClaw
  {
    public override decimal AttackPerSecond => 1.3m;
    public override EnumItem ProfileNumber => EnumItem.CatsPaw;

    public override decimal Rarity => 0.05m;

    public override string? Name => "猫爪刃";
    public override string? Discription => "猫爪刃";
  }
}
