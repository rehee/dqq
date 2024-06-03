using DQQ.Attributes;
using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Affixes.Prefixes.AttackRatings
{
  [Pooled]
  public class AttackRatingProfileT1 : AttackRatingProfile
  {
    public override int TierLevel => 1;
    public override AffixeRange[] Ranges => [AffixeRange.New(EnumPropertyType.AttackRating, 10, 20)];
    public override EnumAffixeNumber ProfileNumber => EnumAffixeNumber.Bronze;
    public override string? Name => "青铜";

    
  }
}
