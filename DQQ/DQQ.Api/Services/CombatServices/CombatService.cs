using DQQ.Api.Services.Itemservices;
using DQQ.Commons.DTOs;
using DQQ.Components.Stages.Maps;
using DQQ.Services.ActorServices;
using DQQ.Services.CombatServices;
using ReheeCmf.Contexts;
using ReheeCmf.Helpers;
using ReheeCmf.Responses;

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
    public async Task<ContentResponse<CombatResultDTO>> RequestCombat(CombatRequestDTO dto)
    {
      var result = new ContentResponse<CombatResultDTO>();
      if (dto?.ActorId == null)
      {
        result.SetNotFound();
        return result;
      }
      var player = await charServices.GetCharacter(dto.ActorId!.Value);
      if (player == null)
      {
        result.SetNotFound();
        return result;
      }
      var map = new Map();
      await map.Initialize(player, dto.MapLevel, dto.SubMapLevel);
      await map.Play();
      if (player.DisplayId != null && map.Drops?.Any() == true)
      {
        await temporaryService.AddAndIntoTemporary(player.DisplayId.Value, map.Drops.ToArray());
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
  }
}
