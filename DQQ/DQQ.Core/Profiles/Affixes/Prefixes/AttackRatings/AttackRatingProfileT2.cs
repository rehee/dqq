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
  public class AttackRatingProfileT2 : AttackRatingProfile
  {
    public override int TierLevel => 2;
    public override int AffixeLevel => 4;

    public override AffixeRange[] Ranges => [AffixeRange.New(EnumPropertyType.AttackRating, 21, 40)];

    public override EnumAffixeNumber ProfileNumber => EnumAffixeNumber.Iron;

    public override string? Name => "铁";

    
  }
}
