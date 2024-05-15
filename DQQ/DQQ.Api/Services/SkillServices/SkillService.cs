using DQQ.Commons.DTOs;
using DQQ.Entities;
using DQQ.Enums;
using DQQ.Pools;
using DQQ.Profiles.Skills;
using DQQ.Services.SkillServices;
using Microsoft.EntityFrameworkCore;
using ReheeCmf.Contexts;
using ReheeCmf.Helpers;
using ReheeCmf.Responses;

namespace DQQ.Api.Services.SkillServices
{
  public class SkillService : ISkillService
  {
    private readonly IContext context;

    public SkillService(IContext context)
    {
      this.context = context;
    }
    public async Task<IEnumerable<ISkill>> GetAllSkills()
    {
      await Task.CompletedTask;
      return DQQPool.SkillPool.Select(b => b.Value).ToArray();
    }

    public async Task<ContentResponse<bool>> PickSkill(PickSkillDTO dto)
    {
      var result = new ContentResponse<bool>();
      var user = this.context?.User?.UserId;
      var actor = await context.Query<ActorEntity>(true).Where(b => b.Id == dto.ActorId && b.OwnerId == user).AnyAsync();
      if (!actor)
      {
        result.SetNotFound();
        return result;
      }
      var existingEntity = await context.Query<SkillEntity>(false).Where(b => b.ActorId == dto.ActorId && (b.Slot == dto.Slot || b.SkillNumber == dto.SkillNumber))
        .ToArrayAsync();
      foreach (var e in existingEntity)
      {
        context.Delete(e);
      }
      await context.SaveChangesAsync();
      var skillEntity = new SkillEntity()
      {
        ActorId = dto.ActorId,
        Slot = dto.Slot ?? 0,
        SkillNumber = dto.SkillNumber,
      };
      await context.AddAsync(skillEntity);
      await context.SaveChangesAsync();
      result.SetSuccess(true);
      return result;
    }
  }
}
