using DQQ.Entities;
using DQQ.Enums;
using DQQ.Services.StrategyServices;
using Microsoft.EntityFrameworkCore;
using ReheeCmf.Contexts;
using ReheeCmf.Helpers;
using ReheeCmf.Responses;

namespace DQQ.Api.Services.StrategyServices
{
  public class StrategyService : IStrategyService
  {
    private readonly IContext context;

    public StrategyService(IContext context)
    {
      this.context = context;
    }

    public async Task<ContentResponse<bool>> SetActorTargetPriority(Guid? actorId, EnumTargetPriority? priority)
    {
      var result = new ContentResponse<bool>();
      var userId = context.User?.UserId;
      var actor = await context.Query<ActorEntity>(false).Where(b => b.OwnerId == userId && b.Id == actorId).FirstOrDefaultAsync();
      if (actor == null)
      {
        return result;
      }
      actor.TargetPriority = priority;
      try
      {
        await context.SaveChangesAsync();
        result.SetSuccess(true);
      }
      catch (Exception ex)
      {
        result.SetError(ex);
      }

      return result;
    }
  }
}
