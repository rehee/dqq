﻿using DQQ.Attributes;
using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Affixes.Prefixes.AttackRatings
{
  [Pooled]
  public class AttackRatingProfileT7 : AttackRatingProfile
  {
    public override int TierLevel => 7;
    public override int AffixeLevel => 27;

    public override AffixeRange[] Ranges => [AffixeRange.New(EnumPropertyType.AttackRating, 121, 150)];

    public override EnumAffixeNumber ProfileNumber => EnumAffixeNumber.Meteoric;

    public override string? Name => "陨铁";


  }
}
