using DQQ.Api.Services.Itemservices;
using DQQ.Commons.DTOs;
using DQQ.Components.Stages.Maps;
using DQQ.Helper;
using DQQ.Services.ActorServices;
using DQQ.Services.ChapterServices;
using DQQ.Services.CombatServices;
using ReheeCmf.Contexts;
using ReheeCmf.Helpers;
using ReheeCmf.Responses;
using System.Numerics;

namespace DQQ.Api.Services.CombatServices
{
	public class CombatService : ICombatService
	{
		private readonly IContext context;
		private readonly ICharacterService charServices;
		private readonly ITemporaryService temporaryService;
		private readonly IChapterService chapterService;

		public CombatService(IContext context, ICharacterService charServices, ITemporaryService temporaryService, IChapterService chapterService)
		{
			this.context = context;
			this.charServices = charServices;
			this.temporaryService = temporaryService;
			this.chapterService = chapterService;
		}

		public Task<ContentResponse<CombatResultDTO>> PushCombatRandom(CombatRequestDTO? dto)
		{
			throw new NotImplementedException();
		}

		public async Task<ContentResponse<CombatResultDTO>> RequestCombat(CombatRequestDTO? dto)
		{
			var result = new ContentResponse<CombatResultDTO>();
			if (dto?.ActorId == null)
			{
				result.SetNotFound();
				return result;
			}
			var player = await charServices.GetCharacter(dto?.ActorId);
			if (player == null)
			{
				result.SetNotFound();
				return result;
			}
			var map = new Map();
			dto.Creator = player;
			await map.Initialize(dto);
			await map.Play();
			if (player.DisplayId != null && map.Drops?.Any() == true)
			{
				await temporaryService.AddAndIntoTemporary(player.DisplayId.Value, map.Drops.ToArray());
			}
			if (map?.XP >= 0)
			{
				await charServices.GainExperience(dto?.ActorId, $"{map?.XP}");
			}
			if (map?.MapClear == true)
			{
				var nextChapter = ChapterHelper.NextChapter(player, map);
				if (nextChapter != player.Chapter)
				{
					await chapterService.ProcessChapter(player.DisplayId, nextChapter);
				}
			}
			if (dto?.RandomGuid != null)
			{
				result.SetSuccess(new CombatResultDTO { });
				return result;
			}
			var resultDto = new CombatResultDTO
			{
				Logs = map!.Logs!.ToArray(),
				XP = map!.XP,
				DropItemNumber = map?.Drops?.Count ?? 0,
				TotalCombatminutes = map!.PlayMins,
				Success = map?.MapClear == true
			};
			result.SetSuccess(resultDto);
			return result;
		}

		public Task<ContentResponse<CombatResultDTO>> RequestCombatRandom(CombatRequestDTO? dto)
		{
			throw new NotImplementedException();
		}
	}
}
