using DQQ.Entities;
using DQQ.Enums;
using DQQ.Services.StrategyServices;
using DQQ.Strategies.SkillStrategies;
using Microsoft.EntityFrameworkCore;
using ReheeCmf.Commons.Jsons.Options;
using ReheeCmf.Contexts;
using ReheeCmf.Helpers;
using ReheeCmf.Responses;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DQQ.Api.Services.StrategyServices
{
  public class StrategyService : IStrategyService
  {
    private readonly IContext context;

    public StrategyService(IContext context)
    {
      this.context = context;
    }

    public async Task<ContentResponse<bool>> SetActorSkillStrategy(Guid? actorId, EnumSkillSlot slot, IEnumerable<SkillStrategy>? strategies)
    {
      var result = new ContentResponse<bool>();
      var userId = context.User?.UserId;
      var actor = await context.Query<ActorEntity>(true).Where(b => b.OwnerId == userId && b.Id == actorId).FirstOrDefaultAsync();
      if (actor == null)
      {
        return result;
      }
      var skill = await context.Query<SkillEntity>(false).Where(b => b.ActorId == actorId && b.Slot == slot).ToArrayAsync();
      if (skill.Length <= 0)
      {
        return result;
      }
      if (skill.Length > 1)
      {
        for (var i = 1; i <= skill.Length; i++)
        {
          context.Delete(skill[i]);
        }
      }
      if (strategies == null || strategies?.Any() != true)
      {
        skill[0].SkillStrategy = null;
      }
      else
      {
        var array = strategies.OrderBy(b => b.Priority).ToArray();
        skill[0].SkillStrategy = JsonSerializer.Serialize(array, JsonOption.DefaultOption);
      }

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
