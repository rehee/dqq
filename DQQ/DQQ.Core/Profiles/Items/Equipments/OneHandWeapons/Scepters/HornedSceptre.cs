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
  public class HornedSceptre : AbScepter
  {
    public override decimal AttackPerSecond => 1.3m;

    public override decimal Rarity => 1;

    public override EnumItem ProfileNumber => EnumItem.HornedSceptre;

    public override string? Name => "犄角短杖";

    public override string? Discription => "犄角短杖";
  }
}
