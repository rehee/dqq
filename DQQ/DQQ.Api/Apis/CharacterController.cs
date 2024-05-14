using DQQ.Components.Stages.Actors.Characters;
using DQQ.Services.ActorServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReheeCmf.Modules.Controllers;

namespace DQQ.Api.Apis
{
  [ApiController]
  [Route("Character")]
  public class CharacterController : ReheeCmfController
  {
    private readonly ICharacterService cService;

    public CharacterController(IServiceProvider sp, ICharacterService cService) : base(sp)
    {
      this.cService = cService;
    }

    [Route("")]
    [HttpGet]
    public async Task<IEnumerable<Character>> GetAllCharacter()
    {
      return await cService.GetAllCharacters();
    }
    [Route("{Id}")]
    [HttpGet]
    public async Task<Character> GetCharacter(Guid? Id)
    {
      if (Id == null)
      {
        return null;
      }
      return await cService.GetCharacter(Id.Value);
    }
    [Route("")]
    [HttpPost]
    public async Task<Guid?> CreateCharacter(Character c)
    {
      await Task.CompletedTask;
      if (String.IsNullOrEmpty(c?.DisplayName))
      {
        return null;
      }
      c.OwnerId = currentUser?.UserId;
      var result = await cService.CreateCharacter(c);
      return result.Content;
    }
  }
}
