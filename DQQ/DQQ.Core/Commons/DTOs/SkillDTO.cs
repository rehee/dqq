using DQQ.Components.Parameters;
using DQQ.Components.Stages;
using DQQ.Components.Stages.Maps;
using DQQ.Enums;
using DQQ.Pools;
using DQQ.Profiles.Skills;
using DQQ.Strategies.SkillStrategies;
using ReheeCmf.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DQQ.Commons.DTOs
{
  public class SkillDTO : ISkill
  {
    public EnumSkill SkillNumber { get; set; }
    [JsonIgnore]
    public SkillProfile? Profile => DQQPool.TryGet<SkillProfile, EnumSkill>(SkillNumber);
    public string? SkillName { get; set; }

    public decimal CastTime { get; set; }

    public bool CastWithWeaponSpeed { get; set; }

    public decimal CoolDown { get; set; }

    public decimal DamageRate { get; set; }

    public string? Discription { get; set; }

    public bool NoPlayerSkill { get; set; }
    public List<SkillStrategy>? SkillStrategies { get; set; }

    public Task<ContentResponse<bool>> CastSkill(ComponentTickParameter? parameter)
    {
      throw new NotImplementedException();
    }
  }
}
