using DQQ.Enums;
using DQQ.Pools;
using DQQ.Profiles.Skills;
using DQQ.Strategies.SkillStrategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DQQ.Profiles.Mobs
{
  public class MobSkill
  {
    public static MobSkill New(EnumSkillNumber skillNumber, params SkillStrategyDTO[]? strategies)
    {
      var skill = new MobSkill();
      skill.SkillNumber = skillNumber;
      if (strategies?.Any() == true)
      {
        skill.Strategies = strategies!.OrderBy(b => b.Priority).ToArray();
      }
      return skill;
    }
    public EnumSkillNumber SkillNumber { get; set; }

    [JsonIgnore]
    public SkillProfile? Profile => DQQPool.TryGet<SkillProfile, EnumSkillNumber>(SkillNumber);

    public SkillStrategyDTO[]? Strategies { get; set; }
  }
}
