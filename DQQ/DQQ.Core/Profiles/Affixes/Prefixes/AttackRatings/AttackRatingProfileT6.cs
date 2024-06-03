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
  public class AttackRatingProfileT6 : AttackRatingProfile
  {
    public override int TierLevel => 6;
    public override int AffixeLevel => 22;

    public override AffixeRange[] Ranges => [AffixeRange.New(EnumPropertyType.AttackRating, 101, 120)];

    public override EnumAffixeNumber ProfileNumber => EnumAffixeNumber.Platinum;

    public override string? Name => "白金";


  }
}
