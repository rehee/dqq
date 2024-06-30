using DQQ.Api.Services.Itemservices;
using DQQ.Commons.DTOs;
using DQQ.Components.Stages.Maps;
using DQQ.Entities;
using DQQ.Services;
using DQQ.Services.ActorServices;
using DQQ.Services.CombatServices;
using DQQ.Web.Datas;
using DQQ.Web.Pages.DQQs.Characters;
using DQQ.Web.Services.Requests;
using ReheeCmf.Helpers;
using ReheeCmf.Requests;
using ReheeCmf.Responses;

namespace DQQ.Web.Services.CombatServices
{
	public class CombatService : ClientServiceBase, ICombatService
	{
		private readonly ITemporaryService tempService;
		private readonly ICharacterService characterService;

		public CombatService(ITemporaryService tempService, ICharacterService characterService, RequestClient<DQQGetHttpClient> client, IIndexRepostory repostory, IGameStatusService statusService) : base(client, repostory, statusService)
		{
			this.tempService = tempService;
			this.characterService = characterService;
		}

		public async Task<ContentResponse<CombatResultDTO>> PushCombatRandom(CombatRequestDTO? dto)
		{
			if (await IsOnleService())
			{
				return await client.Request<CombatResultDTO>(HttpMethod.Post, "Combat/Request", dto.ToJson());
			}
			var result = new ContentResponse<CombatResultDTO>();
			result.SetSuccess(new CombatResultDTO { });
			return result;
		}

		public async Task<ContentResponse<CombatResultDTO>> RequestCombat(CombatRequestDTO? dto)
		{
			return await client.Request<CombatResultDTO>(HttpMethod.Post, "Combat/Request", dto.ToJson());
		}

		public async Task<ContentResponse<CombatResultDTO>> RequestCombatRandom(CombatRequestDTO? dto)
		{
			var result = new ContentResponse<CombatResultDTO>();
			if (dto == null)
			{
				return result;
			}
			var player = await characterService.GetCharacter(dto?.ActorId);
			if (player == null)
			{
				return result;
			}
			dto.Creator = player;
			var map = new Map();
			await map.Initialize(dto);
			await map.Play();

			var resultDto = new CombatResultDTO
			{
				Logs = map!.Logs!.ToArray(),
				XP = map!.XP,
				DropItemNumber = map?.Drops?.Count ?? 0,
				TotalCombatminutes = map!.PlayMins,
				Success = map!.MapClear,
				CombatTimeLimitationTick = map.TotalTick,
				CombatTick = map.TickCount,
				Timelines = map?.TimeLines?.ToArray()
			};
			result.SetSuccess(resultDto);

			if(await IsOnleService())
			{
				return result;
			}
			if (!resultDto.Success)
			{
				return result;
			}

			if (player.DisplayId != null && map?.Drops?.Any() == true)
			{
				await tempService.AddAndIntoTemporary(player.DisplayId.Value, map.Drops.ToArray());
			}

			var nextChapter = ChapterHelper.NextChapter(player, map);
			await Repostory.Update<OfflineCharacter>(dto?.ActorId, c =>
			{
				c.SelectedCharacter.Chapter = nextChapter;
			});
			
			if (map?.XP >= 0)
			{
				await characterService.GainExperience(dto?.ActorId, $"{map?.XP}");
			}
			return result;
		}
	}
}
