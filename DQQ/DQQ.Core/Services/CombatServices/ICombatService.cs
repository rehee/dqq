using DQQ.Commons.DTOs;
using ReheeCmf.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Services.CombatServices
{
  public interface ICombatService
  {
    Task<ContentResponse<CombatResultDTO>> RequestCombat(CombatRequestDTO? dto);
  }
}
