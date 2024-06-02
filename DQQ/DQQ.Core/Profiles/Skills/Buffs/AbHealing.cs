using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Skills.Buffs
{
  public abstract class AbHealing : SkillProfile
  {
    public override EnumSkillType SkillType => EnumSkillType.Healing;
  }
}
