using DQQ.Api.Services.Itemservices;
using DQQ.Components.Stages.Actors.Characters;
using DQQ.Entities;
using DQQ.Enums;
using DQQ.Helper;
using DQQ.Pools;
using DQQ.Profiles.Items;
using DQQ.Profiles.Items.Equipments;
using DQQ.Services.ActorServices;
using DQQ.Services.ItemServices;
using Microsoft.EntityFrameworkCore;
using ReheeCmf.Contexts;
using ReheeCmf.Helpers;
using ReheeCmf.Responses;
using System;
using System.Numerics;
using System.Xml;

namespace DQQ.Api.Services.Characters
{
	public class CharacterService : ICharacterService
	{
		private readonly IContext context;
		//private readonly IItemService itemService;
		//private readonly ITemporaryService tService;

		public CharacterService(IContext context)
		{
			this.context = context;
			
		}
		public async Task<ContentResponse<Guid?>> CreateCharacter(Character? character)
		{
			var result = new ContentResponse<Guid?>();
			var random = RandomHelper.NewRandom();
			try
			{
				if (character == null)
				{
					return result;
				}
				var entity = new ActorEntity();
				entity.OwnerId = character.OwnerId;
				entity.Name = character.DisplayName;
				entity.MaxHP = 50;
				await context.AddAsync(entity);
				await context.SaveChangesAsync();
				result.SetSuccess(entity.Id);
			}
			catch (Exception ex)
			{
				result.SetError(ex);
			}
			return result;
		}

		public Task<ContentResponse<Guid?>> DeleteCharacter(Guid? charId)
		{
			throw new NotImplementedException();
		}

		private IQueryable<ActorEntity> getActor(bool asNoTracking = true)
		{
			var userId = context.User?.UserId;
			return context.Query<ActorEntity>(asNoTracking).Where(b => b.OwnerId == userId);
		}
		public async Task<IEnumerable<Character>> GetAllCharacters()
		{
			var list = await getActor().ToArrayAsync();
			return list.Select(b => b.GenerateTypedComponent<Character>(null));
		}

		public async Task<Character?> GetCharacter(Guid? charId)
		{
			if (charId == null)
			{
				return null;
			}
			var entity = await getActor().Where(b => b.Id == charId).FirstOrDefaultAsync();
			return entity?.GenerateTypedComponent<Character>(null);
		}

		public Task<bool> SelectedCharacter(Guid? charId)
		{
			throw new NotImplementedException();
		}

		public Task<Guid?> GetSelectedCharacter()
		{
			throw new NotImplementedException();
		}

		public async Task<ContentResponse<bool>> GainExperience(Guid? charId, string? exp)
		{
			var response = new ContentResponse<bool>();
			if (!BigInteger.TryParse(exp, out var xpPoint))
			{
				return response;
			}
			if (xpPoint <= 0)
			{
				return response;
			}
			var entity = await getActor(false).Where(b => b.Id == charId).FirstOrDefaultAsync();
			if (entity == null)
			{
				return response;
			}
			BigInteger currentXp = 0;
			BigInteger.TryParse(entity.CurrentXP, out currentXp);
			currentXp = xpPoint + currentXp;
			var levelCheck = XPHelper.CheckExperienceAndLevelUP(ExperienceAndLevel.New(entity.Level, currentXp));
			entity.Level = levelCheck.Level;
			entity.CurrentXP = levelCheck.Experience.ToString();
			await context.SaveChangesAsync();
			return response;
		}
	}
}
