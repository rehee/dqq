using DQQ.Attributes;
using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Affixes.Prefixes.SpearSkills
{
  [Pooled]
  public class SpearSkillProfileT1 : SpearSkillProfile
  {
    public override EnumAffixeNumber ProfileNumber => EnumAffixeNumber.Maiden;
    public override int AffixeLevel => 30;
    public override int TierLevel => 1;
    public override EnumItemType[]? ItemTypeLimites => [EnumItemType.Spear];
    public override EnumEquipType[]? EquipTypeLimites => [EnumEquipType.TwoHandWeapon];
    public override AffixeRange[] Ranges => [AffixeRange.New(EnumPropertyType.DamageModifier, 10)];

    public override string? Name => "处女之";
    public override string? Discription => "攻击力增加 10%";
  }
}
