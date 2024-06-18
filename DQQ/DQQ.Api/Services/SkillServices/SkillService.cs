using DQQ.Commons.DTOs;
using DQQ.Entities;
using DQQ.Enums;
using DQQ.Helper;
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
		public async Task<IEnumerable<SkillDTO>> GetAllSkills()
		{
			await Task.CompletedTask;
			return DQQPool.SkillPool.Select(b => b.Value).PlayerAvaliableSkill().Select(b => new SkillDTO
			{
				SkillNumber = b.SkillNumber,
			}).ToArray();
		}

		public async Task<ContentResponse<bool>> PickSkill(PickSkillDTO? dto)
		{
			var result = new ContentResponse<bool>();
			if (dto == null)
			{
				return result;

			}
			var user = this.context?.User?.UserId;
			var actor = await context!.Query<ActorEntity>(true).Where(b => b.Id == dto.ActorId && b.OwnerId == user).AnyAsync();

			if (!actor)
			{
				return result;
			}

			if (dto.SkillNumber != null)
			{
				var skillPick = DQQPool.TryGet<SkillProfile, EnumSkillNumber?>(dto.SkillNumber);
				if (skillPick?.IsPlayerAvaliableSkill(null) != true)
				{
					await deleteSkill(context, dto.ActorId, false, [dto.Slot ?? EnumSkillSlot.NotSpecified], [dto.SkillNumber]);
					await context.SaveChangesAsync();
					result.SetSuccess(true);
					return result;
				}
			}
			if (!actor)
			{
				result.SetNotFound();
				return result;
			}


			await deleteSkill(context, dto.ActorId, false, [dto.Slot ?? EnumSkillSlot.NotSpecified], [dto.SkillNumber]);
			await createSkill(context, dto, dto.ActorId);
			await context.SaveChangesAsync();
			result.SetSuccess(true);
			return result;
		}


		public async Task<ContentResponse<bool>> PickSkills(Guid? actorId, params PickSkillDTO?[] dtos)
		{
			var result = new ContentResponse<bool>();
			if (dtos?.Any() != true)
			{
				return result;

			}
			var user = this.context?.User?.UserId;
			var actor = await context!.Query<ActorEntity>(true).Where(b => b.Id == actorId && b.OwnerId == user).AnyAsync();
			if (!actor)
			{
				return result;
			}
			var slots = dtos?.Where(b => b?.Slot != null).Select(b => b!.Slot!.Value).ToArray();
			var skillNumbers = dtos?.Select(b => b?.SkillNumber).ToArray();
			await deleteSkill(context, actorId, false, slots ?? [], skillNumbers ?? []);

			if (dtos?.Any() == true)
			{
				foreach (var dto in dtos)
				{
					await createSkill(context, dto, actorId);
				}
			}
			await context.SaveChangesAsync();
			result.SetSuccess(true);
			return result;
		}
		public static async Task createSkill(IContext context, PickSkillDTO? dto, Guid? actorId)
		{
			if (dto == null || actorId == null || dto?.Slot == null || dto?.Slot == EnumSkillSlot.NotSpecified)
			{
				return;
			}
			var array = dto.Strategies?.OrderBy(b => b.Priority).ToArray();
			var str = JsonSerializer.Serialize(array, JsonOption.DefaultOption);
			var validSkillNumbers = dto?.SupportSkill?.Select(b => SkillDTO.New(b)).Where(b => b.Profile?.IsPlayerAvaliableSkill() == true)
				.Select(b => b.SkillNumber).Distinct().Take((dto.Slot).MaxSkillNumber()).ToArray();
			var supportSkills = JsonSerializer.Serialize(validSkillNumbers, JsonOption.DefaultOption);

			var skillEntity = new SkillEntity()
			{
				ActorId = actorId,
				Slot = dto?.Slot ?? EnumSkillSlot.MainSlot,
				SkillNumber = dto?.SkillNumber,
				SkillStrategy = str,
				SupportSkills = supportSkills,

			};
			await context.AddAsync(skillEntity);
		}
		public static async Task deleteSkill(IContext context, Guid? actorId, bool save, IEnumerable<EnumSkillSlot> slots, IEnumerable<EnumSkillNumber?> skillNumbers)
		{
			if (actorId == null)
			{
				return;
			}
			var existingEntity = await context.Query<SkillEntity>(false)
				.Where(b => b.ActorId == actorId && (b.Slot == EnumSkillSlot.NotSpecified || slots.Contains(b.Slot) || b.SkillNumber == null || b.SkillNumber == EnumSkillNumber.NotSpecified || skillNumbers.Contains(b.SkillNumber)))
				.ToArrayAsync();
			foreach (var e in existingEntity)
			{
				context.Delete(e);
			}
			if (save)
			{
				await context.SaveChangesAsync();
			}
		}

	}
}
