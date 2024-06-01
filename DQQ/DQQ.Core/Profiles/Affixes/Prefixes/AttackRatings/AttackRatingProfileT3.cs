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
  public class AttackRatingProfileT3 : AttackRatingProfile
  {
    public override int TierLevel => 3;
    public override int AffixeLevel => 8;

    public override AffixeRange[] Ranges => new[] { AffixeRange.New(EnumPropertyType.AttackRating, 41, 60) };

    public override EnumAffixeNumber ProfileNumber => EnumAffixeNumber.Iron;

    public override string? Name => "钢";


  }
}
