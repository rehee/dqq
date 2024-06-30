using DQQ.Components.Stages.Actors;
using DQQ.Enums;
using DQQ.Strategies.SkillStrategies;
using ReheeCmf.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Services.StrategyServices
{
  public interface IStrategyService
  {
    Task<ContentResponse<bool>> SetActorTargetPriority(Guid? actorId, EnumTargetPriority? priority);
    Task<ContentResponse<bool>> SetActorSkillStrategy(Guid? actorId, EnumSkillSlot slot, IEnumerable<SkillStrategyDTO>? strategies);
  }
}
