using Blazor.Serialization.Extensions;
using DQQ.Components.Stages.Actors.Characters;
using DQQ.Services.ActorServices;
using DQQ.Web.Services.Requests;
using Microsoft.JSInterop;
using ReheeCmf.Requests;
using ReheeCmf.Responses;

namespace DQQ.Web.Services.Characters
{
  public class CharacterService : ClientServiceBase, ICharacterService
  {
    private readonly ILocalStorageService local;

    public CharacterService(RequestClient<DQQGetHttpClient>? client, ILocalStorageService local) : base(client)
    {
      this.local = local;
    }

    public async Task<ContentResponse<Guid?>> CreateCharacter(Character character)
    {
      var result = await client.Request<Guid?>(HttpMethod.Post, "Character", character.ToJson());
      return result;
    }

    public Task<ContentResponse<Guid?>> DeleteCharacter(Guid charId)
    {
      throw new NotImplementedException();
    }

    public async Task<IEnumerable<Character>> GetAllCharacters()
    {
      var result = await client.Request<IEnumerable<Character>>(HttpMethod.Get, "Character");
      return result.Content ?? Enumerable.Empty<Character>();
    }

    public async Task<Character?> GetCharacter(Guid charId)
    {
      return (await client.Request<Character?>(HttpMethod.Get, $"Character/{charId}")).Content;
    }

    public Guid? GetSelectedCharacter()
    {
      return local.GetItem<Guid?>("localChar");
    }

    public bool SelectedCharacter(Guid? charId)
    {
      local.SetItem<Guid?>("localChar", charId);
      return true;
    }
  }
}
