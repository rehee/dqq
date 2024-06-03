using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Affixes.Suffixes.EnhancedDamages
{
  public abstract class EnhancedDamageSufixe : SuffixProfile
  {
    public override EnumAffixeGroup AffixeGroup => EnumAffixeGroup.EnhancedDamage;
    public override EnumEquipType[]? EquipTypeLimites => [EnumEquipType.OneHandWeapon, EnumEquipType.MainHandWeapon, EnumEquipType.TwoHandWeapon];
  }
}
