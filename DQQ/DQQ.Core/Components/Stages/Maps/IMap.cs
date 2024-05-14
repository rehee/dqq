using DQQ.Components.Items;
using DQQ.Components.Stages.Actors;
using DQQ.TickLogs;
using System.Numerics;

namespace DQQ.Components.Stages.Maps
{
  public interface IMap : IDQQComponent
  {
    int MapLevel { get; }
    int Tier { get; }
    int SubTier { get; }
    decimal DropQuality { get; }
    decimal DropQuantity { get; }
    int? limitMinute { get; }
    IEnumerable<IActor>? Players { get; }
    IEnumerable<IEnumerable<IActor>?>? MobPool { get; }
    bool Playable { get; }
    bool Playing { get; }
    DateTime? PlayTime { get; }
    decimal PlayMins { get; }
    bool ReopenBlocked { get; }
    Task Play();
    Task Initialize(IDQQComponent creator, int mapTier, int mapSubTier);
    IEnumerable<IItem>? ItemPool { get; }
    List<TickLogItem> Logs { get; }
    List<ItemComponent>? Drops { get; }
    decimal PlayingCurrentSecond { get; }
    Int64 XP { get; set; }
  }
}
