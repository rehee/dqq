using DQQ.Attributes;
using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Items.Equipments.OneHandWeapons.Daggers
{
  [Pooled]
  public class Sai : AbDagger
  {
    public override decimal AttackPerSecond => 1.35m;

    public override decimal Rarity => 1m;

    public override EnumItem ProfileNumber => EnumItem.Sai;

    public override string? Name => "战叉";

    public override string? Discription => "战叉";
  }
}
