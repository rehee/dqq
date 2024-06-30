using BootstrapBlazor.Components;
using DQQ.Combats;
using DQQ.Components.Items;
using DQQ.Components.Items.Equips;
using DQQ.Components.Stages.Actors.Characters;
using DQQ.Entities;
using DQQ.Enums;
using DQQ.Pools;
using DQQ.Services;
using DQQ.Services.ActorServices;
using DQQ.Web.Datas;
using DQQ.Web.Services.Requests;
using ReheeCmf.Helpers;
using ReheeCmf.Requests;
using ReheeCmf.Responses;
using System.Collections.Generic;
using System.Numerics;
using System.Text.Json;

namespace DQQ.Web.Services.Characters
{
	public class CharacterService : ClientServiceBase, ICharacterService
  {
   
    public CharacterService(RequestClient<DQQGetHttpClient> client, IIndexRepostory repostory, IGameStatusService statusService) : base(client, repostory, statusService)
		{
      
    }

    public async Task<ContentResponse<Guid?>> CreateCharacter(Character? character)
    {
      if (character == null)
      {
        return new ContentResponse<Guid?>();
      }
      if(await IsOnleService())
      {
				var response = await client.Request<Guid?>(HttpMethod.Post, "Character", JsonSerializer.Serialize(character));
				return response;
			}
      else
      {
        var offlineCharacter = new OfflineCharacter();
        offlineCharacter.SelectedCharacter = character;
				offlineCharacter.Id=Guid.NewGuid();

        var actor = new ActorEntity();
        actor.Id = offlineCharacter.Id;
        actor.MaxHP = 50;
        actor.Name = character?.DisplayName;
        offlineCharacter.SelectedCharacter = actor.GenerateTypedComponent<Character>(null);
        
        var sword = EnumItem.CopperSword.GenerateItemComponent(new Random(), 1, 1);
        var swordEnttity = sword.ToEntity();
        
        var component = swordEnttity.GenerateTypedComponent<EquipComponent>(null);

        offlineCharacter?.Equip(sword?.ToEntity(), EnumEquipSlot.MainHand);
				offlineCharacter?.TotalEquipProperty();

				await Repostory.Create(offlineCharacter);
			}
      var result = new ContentResponse<Guid?>();
      result.SetSuccess(character?.DisplayId);
			return result;

		}

    public Task<ContentResponse<Guid?>> DeleteCharacter(Guid? charId)
    {
      throw new NotImplementedException();
    }

		public async Task<ContentResponse<bool>> GainExperience(Guid? charId, string? exp)
		{
      if(await IsOnleService())
      {
				throw new NotImplementedException();
			}
      var result = new ContentResponse<bool>();

			var character = await Repostory.GetCurrentOfflineCharacter(charId);
      if (character == null)
      {
        return result;
      }
			if (!BigInteger.TryParse(exp, out var xpPoint))
			{
				return result;
			}
			if (xpPoint <= 0)
			{
				return result;
			}
			BigInteger currentXp = 0;
			BigInteger.TryParse(character?.SelectedCharacter?.CurrentXP?? "0", out currentXp);
			currentXp = xpPoint + currentXp;
			var levelCheck = XPHelper.CheckExperienceAndLevelUP(ExperienceAndLevel.New(character?.SelectedCharacter?.Level??1, currentXp));
			await Repostory.Update<OfflineCharacter>(charId, c => {
			  c.SelectedCharacter.Level = levelCheck.Level;
				c.SelectedCharacter.CurrentXP = levelCheck.Experience.ToString();
			});
			return result;
		}

		public async Task<IEnumerable<Character>> GetAllCharacters()
    {
      if(await IsOnleService())
      {
				var result = await client.Request<IEnumerable<Character>>(HttpMethod.Get, "Character");
				return result.Content ?? Enumerable.Empty<Character>();
			}
      else
      {
        var result = (await Repostory.Read<OfflineCharacter>()).Select(b=>b.SelectedCharacter).Where(b=>b!= null).Select(b=>b!);
        return result;
			}
      
    }

    public async Task<Character?> GetCharacter(Guid? charId)
    {
      if (charId == null)
      {
        return null;
      }
      if (await IsOnleService())
      {
				return (await client.Request<Character?>(HttpMethod.Get, $"Character/{charId}")).Content;
			}
			else
      {
				return (await GetAllCharacters())?.Where(b=>b.DisplayId==charId)?.FirstOrDefault();
			}
			
    }

    public async Task<Guid?> GetSelectedCharacter()
    {
      var status = await StatusService.GetOrCreateGameStatus();
      return status?.Content?.CurrentCharId;
    }

    public async Task<bool> SelectedCharacter(Guid? charId)
    {
			await StatusService.UpdateGameStatus(b => { b.CurrentCharId = charId; });
			return true;
    }
  }
}
