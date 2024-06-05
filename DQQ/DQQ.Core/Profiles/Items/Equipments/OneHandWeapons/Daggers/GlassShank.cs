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
  public class GlassShank : AbDagger
  {
    public override decimal AttackPerSecond => 1.5m;

    public override decimal Rarity => 1m;

    public override EnumItem ProfileNumber => EnumItem.GlassShank;

    public override string? Name => "玻璃碎片";

    public override string? Discription => "玻璃碎片";
  }
}
