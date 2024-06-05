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
  public class OpalSceptre : AbScepter
  {
    public override decimal AttackPerSecond => 1.25m;

    public override decimal Rarity => 1;

    public override EnumItem ProfileNumber => EnumItem.OpalSceptre;

    public override string? Name => "灵石短杖";

    public override string? Discription => "灵石短杖";
  }
}
