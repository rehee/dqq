using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Skills
{
  public abstract class GeneralSkill : SkillProfile
  {
    public override bool NoPlayerSkill => false;
  }
}
