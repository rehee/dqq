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
  public class HellionsPaw : AbClaw
  {
    public override decimal AttackPerSecond => 1.6m;
    public override EnumItem ProfileNumber => EnumItem.HellionsPaw;

    public override decimal Rarity => 0.05m;

    public override string? Name => "白灵之爪";
    public override string? Discription => "白灵之爪";
  }
}
