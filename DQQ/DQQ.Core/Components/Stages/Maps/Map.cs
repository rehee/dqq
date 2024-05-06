using DQQ.Components.Items;
using DQQ.Components.Stages.Actors;
using DQQ.Components.Stages.Actors.Mobs;
using DQQ.Consts;
using DQQ.Entities;
using DQQ.Pools;
using DQQ.TickLogs;
using DQQ.Helper;
using System.Numerics;

namespace DQQ.Components.Stages.Maps
{
  public class Map : IMap
  {
    public int MapLevel { get; set; }
    public int Tier { get; set; }
    public int SubTier { get; set; }
    public decimal DropQuality { get; set; }

    public decimal DropQuantity { get; set; }

    public int? limitMinute { get; set; }

    public IEnumerable<IActor>? Players { get; set; }

    public IEnumerable<IEnumerable<IActor>?>? MobPool { get; set; }



    public IEnumerable<IItem>? ItemPool { get; set; }

    public Guid? DisplayId { get; set; }

    public string? DisplayName { get; set; }

    public IDQQEntity? Profile { get; set; }

    public async Task Initialize(IDQQComponent creator, int mapTier, int mapSubTier)
    {
      await Task.CompletedTask;
      Tier = mapTier;
      SubTier = mapSubTier;
      Playable = true;
      if (Tier > 0)
      {
        MapLevel = DQQGeneral.MinimunMapLevel + DQQGeneral.LevelPerTier * Tier + DQQGeneral.LevelPerSubTier * SubTier;
      }
      else
      {
        if (creator is Actor createActors)
        {
          MapLevel = createActors.Level <= 0 ? 1 : createActors.Level;
        }
        else
        {
          MapLevel = 1;
        }
      }
      if (creator is Actor createActor)
      {
        Players = new IActor[] { createActor };
      }
      var mobList = new List<List<IActor>>();
      MobPool = mobList;
      for (var i = 1; i < 11; i++)
      {
        var wave = new List<IActor>();
        mobList.Add(wave);
        var mob = DQQPool.MobPool.Select(b => new { r = RandomHelper.GetRandom(1), b = b }).OrderByDescending(b => b.r).Select(b => b.b.Value).FirstOrDefault();
        if (i == 10)
        {
          wave.Add(Monster.Create(mob, MapLevel, Enums.EnumMobRarity.Boss));
          continue;
        }
        if (i % 4 == 0)
        {
          wave.Add(Monster.Create(mob, MapLevel, Enums.EnumMobRarity.Champion));
          continue;
        }
        wave.Add(Monster.Create(mob, MapLevel, Enums.EnumMobRarity.Normal));
      }
    }

    public Task Initialize(IDQQEntity profile)
    {
      throw new NotImplementedException();
    }
    private int TickCount { get; set; } = 0;

    public bool Playing { get; set; }
    public bool Playable { get; set; }
    public DateTime? PlayTime { get; set; }
    public decimal PlayMins => TickCount / (DQQGeneral.TickPerSecond * 60m);
    public bool ReopenBlocked => Playing || (PlayTime != null && PlayTime?.AddMinutes((double)PlayMins) <= DateTime.UtcNow);
    public List<TickLogItem>? Logs { get; set; }
    public decimal PlayingCurrentSecond { get; set; }
    public List<ItemComponent>? Drops { get; set; }
    public BigInteger XP { get; set; }

    

    public async Task Play()
    {
      if (!Playable)
      {
        return;
      }
      PlayTime = DateTime.UtcNow;
      Playable = false;
      Playing = true;
      TickCount = 0;
      Logs = new List<TickLogItem>();
      Drops = new List<ItemComponent>();
      while (TickCount < this.TotalTick())
      {
        TickCount++;
        PlayingCurrentSecond = TickCount / (decimal)DQQGeneral.TickPerSecond;
        if (Players == null || MobPool == null)
        {
          break;
        }
        var currentPack = MobPool.FirstOrDefault(b => b != null && b.Any(m => m.Alive));
        if (currentPack == null || currentPack.Any() != true)
        {
          break;
        }
        try
        {
          foreach (var p in Players)
          {
            if (p.Target == null || p.Target.Alive == false)
            {
              p.SelectTarget(currentPack.Where(b => b.Targetable && b.Alive).FirstOrDefault());
            }

            await p.OnTick(currentPack, this);

          }
          foreach (var p in currentPack)
          {
            if (!p.Alive)
            {
              continue;
            }
            if (p.Target == null)
            {
              p.SelectTarget(Players.FirstOrDefault());
            }

            await p.OnTick(Players, this);
          }
        }
        catch
        {
          break;
        }

      }
      Playing = false;
    }
  }
}
