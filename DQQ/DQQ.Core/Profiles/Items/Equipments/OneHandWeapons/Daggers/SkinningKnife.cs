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
  public class SkinningKnife : AbDagger
  {
    public override decimal AttackPerSecond => 1.45m;

    public override decimal Rarity => 1m;

    public override EnumItem ProfileNumber => EnumItem.SkinningKnife;

    public override string? Name => "剥皮刀";

    public override string? Discription => "剥皮刀";
  }
}
