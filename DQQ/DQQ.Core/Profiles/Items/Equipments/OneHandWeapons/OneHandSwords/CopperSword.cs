using DQQ.Attributes;
using DQQ.Components.Parameters;
using DQQ.Enums;
using DQQ.Profiles.Affixes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Items.Equipments.OneHandWeapons.OneHandSwords
{
  [Pooled]
  public class CopperSword : AbOneHandSword
  {
    
    public override decimal AttackPerSecond => 1.3m;

    public override AffixeRange[]? Range => [AffixeRange.New(EnumPropertyType.AttackRating, 6, 460, EnumAffixeRangeType.LevelBased, 6.5m)];
    public override int DropQuantity => 1;
    public override decimal Rarity => 1m;
    public override EnumItem ProfileNumber => EnumItem.CopperSword;
    public override string? Name => "青铜短剑";
    public override string? Discription => "青铜短剑";


  }
}
