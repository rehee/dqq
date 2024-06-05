using DQQ.Attributes;
using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Items.Equipments.OneHandWeapons.Scepters
{
  [Pooled]
  public class QuartzSceptre : AbScepter
  {
    public override decimal AttackPerSecond => 1.1m;

    public override decimal Rarity => 1;

    public override EnumItem ProfileNumber => EnumItem.QuartzSceptre;

    public override string? Name => "石英短杖";

    public override string? Discription => "石英短杖";
  }
}
