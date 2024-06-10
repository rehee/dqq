using DQQ.Commons.DTOs;
using DQQ.Components.Stages.Actors;
using DQQ.Components.Stages.Actors.Characters;
using DQQ.Entities;
using DQQ.Services.ActorServices;
using DQQ.Services.BDServices;
using DQQ.Services.ItemServices;
using DQQ.Services.SkillServices;
using DQQ.Services.StrategyServices;
using Microsoft.EntityFrameworkCore;
using ReheeCmf.Contexts;
using ReheeCmf.Helpers;
using ReheeCmf.Responses;

namespace DQQ.Api.Services.BDServices
{
	public class BDService : IBDService
	{
		private readonly IContext context;
		private readonly ICharacterService characterService;
		private readonly IItemService itemService;
		private readonly ISkillService skillService;
		private readonly IStrategyService strategyService;

		public BDService(IContext context, ICharacterService characterService, IItemService itemService, ISkillService skillService, IStrategyService strategyService)
		{
			this.context = context;
			this.characterService = characterService;
			this.itemService = itemService;
			this.skillService = skillService;
			this.strategyService = strategyService;
		}
		public async Task<ContentResponse<bool>> ApplyBuild(BuildDTO dto)
		{
			var result = new ContentResponse<bool>();
			var entity = await findBuild(dto);
			if (entity?.Item1 == null || entity?.Item2 == null)
			{
				return result;
			}
			var actorId = entity.Value.Item2.DisplayId;
			var allEquips = entity.Value.Item1.Equips;
			var allSlots = allEquips.Select(b => b.Slot).ToArray();
			await itemService.UnEquipItem(actorId, allSlots);
			await itemService.EquipItems(actorId, allEquips.Where(b => b.Id.HasValue).ToArray() ?? []);

			var skillPicks = entity.Value.Item1.SkillMap?.Select(b => PickSkillDTO.New(b.Value, actorId, b.Key)).ToArray();
			if (skillPicks?.Any() == true)
			{
				await skillService.PickSkills(actorId, skillPicks ?? []);
			}
			await strategyService.SetActorTargetPriority(actorId, entity.Value.Item1.TargetPriority);
			result.SetSuccess(true);
			return result;
		}

		public async Task<ContentResponse<bool>> CreateNewBuild(BuildDTO dto)
		{
			var result = new ContentResponse<bool>();
			var cha = await characterService.GetCharacter(dto.ActorId);
			if (cha == null)
			{
				return result;
			}
			var build = ActorBuild.New(cha, dto);
			await context.AddAsync(build);
			await context.SaveChangesAsync();
			result.SetSuccess(true);
			return result;
		}

		public async Task<ContentResponse<bool>> DeleteBuild(BuildDTO dto)
		{
			var result = new ContentResponse<bool>();
			var entity = await findBuild(dto, false);
			if (entity?.Item1 == null || entity?.Item2 == null)
			{
				return result;
			}
			context.Delete(entity.Value.Item1);
			await context.SaveChangesAsync();
			result.SetSuccess(true);
			return result;
		}

		public async Task<IEnumerable<BuildSummaryDTO>?> GetAllBuild(Guid? actorId)
		{
			var cha = await characterService.GetCharacter(actorId);
			if (cha == null)
			{
				return [];
			}
			var bds = await context!.Query<ActorBuild>(true).Where(b => b.ActorId == actorId).ToArrayAsync();
			var result = new List<BuildSummaryDTO>();
			var allEquipmentIds = bds.SelectMany(b => b.Equips).Select(b => b.Id).Where(b => b != null).Distinct().Select(b => b!.Value).ToArray() ?? [];
			var allEquipments = await context.Query<ItemEntity>(true).Where(b => allEquipmentIds.Contains(b.Id)).ToArrayAsync();


			foreach (var bd in bds)
			{
				var equipMap = bd.Equips.ToDictionary(b => b.Slot, b => allEquipments.FirstOrDefault(a => a.Id == b.Id));
				var skillMap = bd.SkillMap;
				result.Add(new BuildSummaryDTO
				{
					ActorId = actorId,
					BuildDescription = bd.BuildDescribe,
					BuildId = bd.Id,
					BuildName = bd.Name,
					Equips = equipMap,
					Skills = skillMap,
					TargetPriority = bd.TargetPriority,
				});
			}

			return result;
		}

		private async Task<(ActorBuild, Character)?> findBuild(BuildDTO dto, bool asNoTracking = true)
		{
			if (dto.BuildId == null)
			{
				return null;
			}
			var cha = await characterService.GetCharacter(dto.ActorId);
			if (cha == null)
			{
				return null;
			}
			var build = await context.Query<ActorBuild>(asNoTracking).FirstOrDefaultAsync(b => b.Id == dto.BuildId);
			return (build!, cha!);
		}

		public async Task<ContentResponse<bool>> UpdateBuild(BuildDTO dto)
		{
			var result = new ContentResponse<bool>();
			var build = await findBuild(dto, false);
			if (build == null)
			{
				return result;
			}
			ActorBuild.SetActorBuild(build.Value.Item1, build.Value.Item2);
			await context.SaveChangesAsync();
			result.SetSuccess(true);
			return result;
		}
	}
}
