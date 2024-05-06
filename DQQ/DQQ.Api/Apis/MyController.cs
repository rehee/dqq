using DQQ.Components.Skills;
using DQQ.Components.Stages.Actors;
using DQQ.Components.Stages.Actors.Characters;
using DQQ.Components.Stages.Maps;
using DQQ.Enums;
using DQQ.Services.MapServices;
using DQQ.TickLogs;
using Microsoft.AspNetCore.Mvc;
using ReheeCmf.Modules.Controllers;

namespace DQQ.Api.Apis
{
  [ApiController]
  [Route("My")]
  public class MyController : ReheeCmfController
  {
    private readonly IMapService mapService;

    public MyController(IServiceProvider sp, IMapService mapService) : base(sp)
    {
      this.mapService = mapService;
    }

    [Route("map")]
    public async Task<IEnumerable<TickLogItem>?> GetMap()
    {
      var map = new Map();
      var creator = new Character();

      creator.Alive = true;
      creator.DisplayName = "player 1";
      creator.CurrentHP = 1000;
      creator.MaximunLife = 1000;
      creator.BasicDamage = 10;
      creator.Skills = new Dictionary<int, ISkillComponent?>
      {
        [0] = SkillComponent.New(EnumSkill.NormalAttack)
      };
      creator.MainHand = 100;
      creator.OffHand = 10;
      creator.AttackPerSecond = 1m;
      await map.Initialize(creator, 1000, 0);
      await map.Play();
      return map.Logs;
    }
  }
}
