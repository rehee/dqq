using DQQ.Enums;
using DQQ.Strategies.SkillStrategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Mobs
{
  public class MobSkill
  {
    public static MobSkill New(EnumSkill skillNumber, params SkillStrategy[]? strategies)
    {
      var skill = new MobSkill();
      skill.SkillNumber = skillNumber;
      if (strategies?.Any() == true)
      {
        skill.Strategies = strategies!.OrderBy(b => b.Priority).ToArray();
      }
      return skill;
    }
    public EnumSkill SkillNumber { get; set; }

    public SkillStrategy[]? Strategies { get; set; }
  }
}
