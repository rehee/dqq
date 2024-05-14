using DQQ.Commons.DTOs;
using DQQ.Services.CombatServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReheeCmf.Modules.Controllers;

namespace DQQ.Api.Apis
{
  [Route("Combat")]
  [ApiController]
  public class CombatController : ReheeCmfController
  {
    private readonly ICombatService combatService;

    public CombatController(IServiceProvider sp, ICombatService combatService) : base(sp)
    {
      this.combatService = combatService;
    }

    [HttpPost]
    [Route("Request")]
    public async Task<CombatResultDTO?> RequestCombat([FromBody] CombatRequestDTO dto)
    {
      var result = await combatService.RequestCombat(dto);
      return result.Content;
    }
  }
}
