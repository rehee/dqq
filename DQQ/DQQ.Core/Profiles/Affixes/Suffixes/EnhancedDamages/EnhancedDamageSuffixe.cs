using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Affixes.Suffixes.EnhancedDamages
{
  public abstract class EnhancedDamageSuffixe : SuffixProfile
  {
    public override EnumAffixeGroup AffixeGroup => EnumAffixeGroup.EnhancedDamage;
		public override EnumEquipType[]? EquipTypeLimites => [
			EnumEquipType.Amulet,EnumEquipType.Ring,EnumEquipType.Belt,EnumEquipType.Offhand,EnumEquipType.Helmet,EnumEquipType.Glove
			];
		public override string? Discription => "伤害增加";
	}
}
