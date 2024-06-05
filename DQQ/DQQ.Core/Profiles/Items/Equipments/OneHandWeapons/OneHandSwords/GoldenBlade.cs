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
  public class GoldenBlade : AbOneHandSword
  {
    
    public override decimal AttackPerSecond => 1.1m;

    public override AffixeRange[]? Range => [AffixeRange.New(EnumPropertyType.AttackRating, 6, 460, EnumAffixeRangeType.LevelBased, 6.5m)];
    public override int DropQuantity => 1;
    public override decimal Rarity => 1m;
    public override EnumItem ProfileNumber => EnumItem.GoldenBlade;
    public override string? Name => "金色之刃";
    public override string? Discription => "金色之刃";


  }
}
