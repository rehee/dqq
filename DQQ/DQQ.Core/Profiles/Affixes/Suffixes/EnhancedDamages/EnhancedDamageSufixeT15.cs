using DQQ.Attributes;
using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Affixes.Suffixes.EnhancedDamages
{
  [Pooled]
  public class EnhancedDamageSufixeT15 : EnhancedDamageSufixe
  {
    public override int AffixeLevel => 45;
    public override int TierLevel => 15;
    public override EnumAffixeNumber ProfileNumber => EnumAffixeNumber.Evisceration;
    public override EnumRarity[]? RarityLimits => [EnumRarity.Magic];
    public override AffixeRange[] Ranges => [AffixeRange.New(EnumPropertyType.Damage, 41, 63)];

    public override string? Name => "吸精";
    public override string? Discription => "伤害增加";
  
  }
}
