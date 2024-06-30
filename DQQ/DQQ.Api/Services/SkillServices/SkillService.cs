using DQQ.Commons.DTOs;
using DQQ.Components.Stages.Actors.Characters;
using DQQ.Entities;
using DQQ.Enums;
using DQQ.Helper;
using DQQ.Pools;
using DQQ.Profiles.Skills;
using DQQ.Services.ActorServices;
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
		private readonly ICharacterService characterService;

		public SkillService(IContext context,ICharacterService characterService)
		{
			this.context = context;
			this.characterService = characterService;
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
		
			var actor = await characterService.GetCharacter(dto?.ActorId);

			if (actor==null)
			{
				return result;
			}

			if (dto?.SkillNumber != null)
			{
				var skillPick = DQQPool.TryGet<SkillProfile, EnumSkillNumber?>(dto.SkillNumber);
				if (skillPick?.IsPlayerAvaliableSkill(actor) != true)
				{
					await deleteSkill(context, dto.ActorId, false, [dto.Slot ?? EnumSkillSlot.NotSpecified], [dto.SkillNumber]);
					await context.SaveChangesAsync();
					result.SetSuccess(true);
					return result;
				}
			}
			


			await deleteSkill(context, dto.ActorId, false, [dto.Slot ?? EnumSkillSlot.NotSpecified], [dto.SkillNumber]);
			await createSkill(context, dto, actor);
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
			var actor = await characterService.GetCharacter(actorId);
			if (actor==null)
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
					await createSkill(context, dto, actor);
				}
			}
			await context.SaveChangesAsync();
			result.SetSuccess(true);
			return result;
		}
		public static async Task createSkill(IContext context, PickSkillDTO? dto, Character? actor)
		{
			var entity = dto?.ToSkillEntity(actor);
			if (entity == null)
			{
				return;
			}
			
			await context.AddAsync(entity);
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
