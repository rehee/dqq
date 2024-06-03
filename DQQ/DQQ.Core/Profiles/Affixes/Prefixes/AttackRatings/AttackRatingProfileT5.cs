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
  public class AttackRatingProfileT5 : AttackRatingProfile
  {
    public override int TierLevel => 5;
    public override int AffixeLevel => 17;

    public override AffixeRange[] Ranges => [AffixeRange.New(EnumPropertyType.AttackRating, 81, 100)];

    public override EnumAffixeNumber ProfileNumber => EnumAffixeNumber.Gold;

    public override string? Name => "金";


  }
}
