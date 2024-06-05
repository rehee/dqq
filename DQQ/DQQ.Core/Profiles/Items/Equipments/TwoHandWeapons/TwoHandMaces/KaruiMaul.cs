using DQQ.Attributes;
using DQQ.Enums;
using DQQ.Profiles.Items.Equipments.TwoHandWeapons.Bows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Items.Equipments.TwoHandWeapons.TwoHandMaces
{
  [Pooled]
  public class KaruiMaul : AbTwoHandMace
  {
    public override decimal AttackPerSecond => 1m;

    public override decimal Rarity => 1;

    public override EnumItem ProfileNumber => EnumItem.KaruiMaul;

    public override string? Name => "卡鲁重锤";

    public override string? Discription => "卡鲁重锤";
  }
}
