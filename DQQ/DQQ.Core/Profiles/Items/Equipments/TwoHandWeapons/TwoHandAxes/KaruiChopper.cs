using DQQ.Attributes;
using DQQ.Enums;
using DQQ.Profiles.Items.Equipments.TwoHandWeapons.Bows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Items.Equipments.TwoHandWeapons.TwoHandAxes
{
  [Pooled]
  public class KaruiChopper : AbTwoHandAxe
  {
    public override decimal AttackPerSecond => 1.05m;

    public override decimal Rarity => 1;

    public override EnumItem ProfileNumber => EnumItem.KaruiChopper;

    public override string? Name => "卡鲁巨斧";

    public override string? Discription => "卡鲁巨斧";
  }
}
