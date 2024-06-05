using DQQ.Attributes;
using DQQ.Enums;
using DQQ.Profiles.Items.Equipments.TwoHandWeapons.Bows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Items.Equipments.TwoHandWeapons.Spears
{
  [Pooled]
  public class Lance : AbSpear
  {
    public override decimal AttackPerSecond => 1.2m;

    public override decimal Rarity => 1;

    public override EnumItem ProfileNumber => EnumItem.Lance;

    public override string? Name => "长枪";

    public override string? Discription => "长枪";
  }
}
