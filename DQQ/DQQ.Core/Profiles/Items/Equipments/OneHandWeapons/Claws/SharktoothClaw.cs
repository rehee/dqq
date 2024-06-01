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
  public class SharktoothClaw : AbClaw
  {
    public override decimal AttackPerSecond => 1.5m;
    public override EnumItem ProfileNumber => EnumItem.SharktoothClaw;

    public override decimal Rarity => 0.05m;

    public override string? Name => "鲨颚爪";
    public override string? Discription => "鲨颚爪";
    
  }
}
