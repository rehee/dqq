using DQQ.Components.Skills;
using DQQ.Components.Stages.Actors;
using DQQ.Components.Stages.Actors.Characters;
using DQQ.Components.Stages.Maps;
using DQQ.Entities;
using DQQ.Enums;
using DQQ.Services.MapServices;
using DQQ.TickLogs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Linq.Translations;
using ReheeCmf.Components.ChangeComponents;
using ReheeCmf.Contexts;
using ReheeCmf.Helpers;
using ReheeCmf.Modules.Controllers;
using ReheeCmf.Utility.CmfRegisters;
using System.Numerics;

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

    [Route("Item")]
    public async Task<string> Index(CancellationToken ct)
    {
      var actor = new ActorEntity();
      await context.AddAsync(actor, ct);
      await context.SaveChangesAsync();

      var item = new ItemEntity();
      item.ItemNumber = EnumItem.CorrodedBlade;
      await context.AddAsync(item, ct);
      await context.SaveChangesAsync();
      var equip = new ActorEquipmentEntity();
      equip.ItemId = item.Id;
      equip.ActorId = actor.Id;
      equip.EquipSlot = EnumEquipSlot.OffHand;
      await context.AddAsync(equip, ct);
      var resultss = await context.SaveChangesAsync();

      return "";
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
      await map.Initialize(creator, 0, 0);
      await map.Play();
      return map.Logs;
    }

    [Route("items")]
    [EnableQuery]
    public IQueryable<ItemEntity> GetAllEntity()
    {
      return context.Query<ItemEntity>(true).WithTranslations();
    }
  }
}
