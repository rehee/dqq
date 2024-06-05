using DQQ.Attributes;
using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Items.Equipments.TwoHandWeapons.Bows
{
  [Pooled]
  public class DecurveBow : AbBow
  {
    public override decimal AttackPerSecond => 1.25m;

    public override decimal Rarity => 1;

    public override EnumItem ProfileNumber => EnumItem.DecurveBow;

    public override string? Name => "反曲弓";

    public override string? Discription => "反曲弓";
  }
}
