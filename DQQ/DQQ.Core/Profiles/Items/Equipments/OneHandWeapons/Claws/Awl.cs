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
  public class Awl : AbClaw
  {
    public override decimal AttackPerSecond => 1.55m;
    public override EnumItem ProfileNumber => EnumItem.Awl;

    public override decimal Rarity => 0.05m;

    public override string? Name => "凿钉";
    public override string? Discription => "凿钉";
  }
}
