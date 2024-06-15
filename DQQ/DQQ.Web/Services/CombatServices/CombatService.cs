using Blazor.Serialization.Extensions;
using DQQ.Commons.DTOs;
using DQQ.Components.Stages.Maps;
using DQQ.Services.ActorServices;
using DQQ.Services.CombatServices;
using DQQ.Web.Services.Requests;
using ReheeCmf.Helpers;
using ReheeCmf.Requests;
using ReheeCmf.Responses;
using System.Numerics;

namespace DQQ.Web.Services.CombatServices
{
	public class CombatService : ClientServiceBase, ICombatService
	{
		private readonly ICharacterService characterService;

		public CombatService(RequestClient<DQQGetHttpClient> client, ICharacterService characterService) : base(client)
		{
			this.characterService = characterService;
		}

		public async Task<ContentResponse<CombatResultDTO>> PushCombatRandom(CombatRequestDTO? dto)
		{
			return await client.Request<CombatResultDTO>(HttpMethod.Post, "Combat/Request", dto.ToJson());
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
			var map = new Map();
			await map.Initialize(player, dto.MapLevel, dto.SubMapLevel, dto?.RandomGuid);
			await map.Play();

			var resultDto = new CombatResultDTO
			{
				Logs = map!.Logs!.ToArray(),
				XP = map!.XP,
				DropItemNumber = map?.Drops?.Count ?? 0,
				TotalCombatminutes = map!.PlayMins,
				Success = map!.MobPool?.All(b => b.All(c => c.Alive != true)) ?? false
			};
			result.SetSuccess(resultDto);
			return result;
		}
	}
}
