using DQQ.Commons.DTOs;
using DQQ.Entities;
using DQQ.Enums;
using DQQ.Pools;
using DQQ.Profiles.Skills;
using DQQ.Services.SkillServices;
using Microsoft.EntityFrameworkCore;
using ReheeCmf.Commons.Jsons.Options;
using ReheeCmf.Contexts;
using ReheeCmf.Helpers;
using ReheeCmf.Responses;
using System.Text.Json;

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
			return DQQPool.SkillPool.Select(b => b.Value).Where(b => b.NoPlayerSkill != true).ToArray();
		}

		public async Task<ContentResponse<bool>> PickSkill(PickSkillDTO dto)
		{
			var result = new ContentResponse<bool>();
			var user = this.context?.User?.UserId;
			var actor = await context.Query<ActorEntity>(true).Where(b => b.Id == dto.ActorId && b.OwnerId == user).AnyAsync();
			if (dto.SkillNumber != null)
			{
				var skillPick = DQQPool.TryGet<SkillProfile, EnumSkill?>(dto.SkillNumber);
				if (skillPick.NoPlayerSkill)
				{
					return result;
				}
			}
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
			var array = dto.Strategies?.OrderBy(b => b.Priority).ToArray();
			var str = JsonSerializer.Serialize(array, JsonOption.DefaultOption);
			var skillEntity = new SkillEntity()
			{
				ActorId = dto.ActorId,
				Slot = dto.Slot ?? EnumSkillSlot.NotSpecified,
				SkillNumber = dto.SkillNumber,
				SkillStrategy = str,
			};
			await context.AddAsync(skillEntity);
			await context.SaveChangesAsync();
			result.SetSuccess(true);
			return result;
		}
	}
}
