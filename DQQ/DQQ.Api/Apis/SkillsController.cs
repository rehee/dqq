using DQQ.Commons.DTOs;
using DQQ.Profiles.Skills;
using DQQ.Services.SkillServices;
using Microsoft.AspNetCore.Mvc;
using ReheeCmf.Modules.Controllers;

namespace DQQ.Api.Apis
{
  [Route("Skills")]
  [ApiController]
  public class SkillsController : ReheeCmfController
  {
    private readonly ISkillService skillService;

    public SkillsController(IServiceProvider sp, ISkillService skillService) : base(sp)
    {
      this.skillService = skillService;
    }
    [HttpGet]
    public async Task<IEnumerable<ISkill>> Index()
    {
      return await skillService.GetAllSkills();
    }

    [HttpPost]
    public async Task<bool> PickSkill([FromBody] PickSkillDTO dto)
    {
      var result = await skillService.PickSkill(dto);
      return result.Success;
    }
  }
}
