using DQQ.Api.Services.Itemservices;
using DQQ.Components.Stages.Actors.Characters;
using DQQ.Entities;
using DQQ.Enums;
using DQQ.Pools;
using DQQ.Profiles.Items.Equipments;
using DQQ.Services.ActorServices;
using DQQ.Services.ItemServices;
using Microsoft.EntityFrameworkCore;
using ReheeCmf.Contexts;
using ReheeCmf.Helpers;
using ReheeCmf.Responses;
using System.Xml;

namespace DQQ.Api.Services.Characters
{
  public class CharacterService : ICharacterService
  {
    private readonly IContext context;
    private readonly IItemService itemService;
    private readonly ITemporaryService tService;

    public CharacterService(IContext context, IItemService itemService, ITemporaryService tService)
    {
      this.context = context;
      this.itemService = itemService;
      this.tService = tService;
    }
    public async Task<ContentResponse<Guid?>> CreateCharacter(Character character)
    {
      var result = new ContentResponse<Guid?>();
      try
      {
        var entity = new ActorEntity();
        entity.OwnerId = character.OwnerId;
        entity.Name = character.DisplayName;
        entity.MaxHP = 50;
        await context.AddAsync(entity);
        await context.SaveChangesAsync();

        var sword = (DQQPool.ItemPool[EnumItem.CopperSword] as EquipProfile)!.GenerateComponent(1, 1);
        await tService.InsertIntoTemporary(entity.Id, sword);
        
        await itemService.PickItem(entity.Id, sword.DisplayId!.Value);
        await itemService.EquipItem(entity.Id, sword.DisplayId!.Value, EnumEquipSlot.MainHand);

        result.SetSuccess(entity.Id);
      }
      catch (Exception ex)
      {
        result.SetError(ex);
      }
      return result;
    }

    public Task<ContentResponse<Guid?>> DeleteCharacter(Guid charId)
    {
      throw new NotImplementedException();
    }

    private IQueryable<ActorEntity> getActor
    {
      get
      {
        var userId = context.User?.UserId;
        return context.Query<ActorEntity>(true).Where(b => b.OwnerId == userId);
      }
    }
    public async Task<IEnumerable<Character>> GetAllCharacters()
    {
      var list = await getActor.ToArrayAsync();
      return list.Select(b => b.GenerateTypedComponent<Character>());
    }

    public async Task<Character?> GetCharacter(Guid charId)
    {
      var entity = await getActor.Where(b => b.Id == charId).FirstOrDefaultAsync();
      return entity?.GenerateTypedComponent<Character>();
    }

    public bool SelectedCharacter(Guid? charId)
    {
      throw new NotImplementedException();
    }

    public Guid? GetSelectedCharacter()
    {
      throw new NotImplementedException();
    }
  }
}
