using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Affixes.Prefixes.AttackRatings
{
  public abstract class AttackRatingProfile : PrefixProfile
  {
    public override EnumAffixeGroup AffixeGroup => EnumAffixeGroup.AttackRating;
    public override EnumEquipType[]? EquipTypeLimites => [EnumEquipType.OneHandWeapon, EnumEquipType.MainHandWeapon, EnumEquipType.TwoHandWeapon, EnumEquipType.Glove, EnumEquipType.Ring];

    public override string? Discription => "准确率增加";
  }
}
