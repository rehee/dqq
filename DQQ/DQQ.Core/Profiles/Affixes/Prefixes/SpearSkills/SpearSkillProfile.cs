using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Affixes.Prefixes.SpearSkills
{
  public abstract class SpearSkillProfile : PrefixProfile
  {
    public override EnumAffixeGroup AffixeGroup => EnumAffixeGroup.SkillDamage;
  }
}
