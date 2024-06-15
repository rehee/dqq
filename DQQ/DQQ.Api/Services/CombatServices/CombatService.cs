﻿using DQQ.Api.Services.Itemservices;
using DQQ.Commons.DTOs;
using DQQ.Components.Stages.Maps;
using DQQ.Services.ActorServices;
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

		public CombatService(IContext context, ICharacterService charServices, ITemporaryService temporaryService)
		{
			this.context = context;
			this.charServices = charServices;
			this.temporaryService = temporaryService;
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
			await map.Initialize(player, dto?.MapLevel ?? 0, dto?.SubMapLevel ?? 0, dto?.RandomGuid);
			await map.Play();
			if (player.DisplayId != null && map.Drops?.Any() == true)
			{
				await temporaryService.AddAndIntoTemporary(player.DisplayId.Value, map.Drops.ToArray());
			}
			if (map?.XP >= 0)
			{
				await charServices.GainExperience(dto?.ActorId, $"{map?.XP}");
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
				Success = map!.MobPool?.All(b => b.All(c => c.Alive != true)) ?? false
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
