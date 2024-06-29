using DQQ.Components.Stages.Actors.Characters;
using DQQ.Services;
using DQQ.Services.ActorServices;
using DQQ.Web.Services.Requests;
using ReheeCmf.Requests;
using ReheeCmf.Responses;
using System.Text.Json;

namespace DQQ.Web.Services.Characters
{
	public class CharacterService : ClientServiceBase, ICharacterService
  {
    private readonly IGameStatusService gameStatusService;

    public CharacterService(RequestClient<DQQGetHttpClient>? client, IGameStatusService gameStatusService) : base(client)
    {
      this.gameStatusService = gameStatusService;
    }

    public async Task<ContentResponse<Guid?>> CreateCharacter(Character? character)
    {
      if (character == null)
      {
        return new ContentResponse<Guid?>();
      }
      
      var result = await client.Request<Guid?>(HttpMethod.Post, "Character", JsonSerializer.Serialize(character));
      return result;
    }

    public Task<ContentResponse<Guid?>> DeleteCharacter(Guid? charId)
    {
      throw new NotImplementedException();
    }

		public Task<ContentResponse<bool>> GainExperience(Guid? charId, string? exp)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<Character>> GetAllCharacters()
    {
      var result = await client.Request<IEnumerable<Character>>(HttpMethod.Get, "Character");
      return result.Content ?? Enumerable.Empty<Character>();
    }

    public async Task<Character?> GetCharacter(Guid? charId)
    {
      if (charId == null)
      {
        return null;
      }
      return (await client.Request<Character?>(HttpMethod.Get, $"Character/{charId}")).Content;
    }

    public async Task<Guid?> GetSelectedCharacter()
    {
      var status = await gameStatusService.GetOrCreateGameStatus();
      return status?.Content?.CurrentCharId;
    }

    public async Task<bool> SelectedCharacter(Guid? charId)
    {
			var status = await gameStatusService.GetOrCreateGameStatus();
      if (status.Success)
      {
        status!.Content!.CurrentCharId= charId;
				await gameStatusService.UpdateGameStatus(status?.Content);
			}
			return true;
    }
  }
}
