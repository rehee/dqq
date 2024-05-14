using DQQ.Commons.DTOs;
using DQQ.Enums;
using DQQ.Profiles.Skills;
using ReheeCmf.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Services.SkillServices
{
  public interface ISkillService
  {
    Task<IEnumerable<ISkill>> GetAllSkills();
    Task<ContentResponse<bool>> PickSkill(PickSkillDTO dto);
  }
}
