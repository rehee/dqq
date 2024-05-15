using DQQ.Components.Items;
using DQQ.Entities;
using ReheeCmf.Helpers;
using ReheeCmf.Responses;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace DQQ.Api.Services.Itemservices
{
  public class TemporaryService : ITemporaryService
  {
    public static ConcurrentDictionary<Guid, HashSet<ItemComponent>> TemporaryItemPool { get; set; } = new ConcurrentDictionary<Guid, HashSet<ItemComponent>>();
    public async Task<IEnumerable<ItemEntity>> GetAllTemporaryItems(Guid? actorId)
    {
      await Task.CompletedTask;
      if (actorId == null)
      {
        return Enumerable.Empty<ItemEntity>();
      }
      if (TemporaryItemPool.TryGetValue(actorId.Value, out var items))
      {
        return items.Select(b => b.ToEntity());
      }
      var set = new HashSet<ItemComponent>();
      TemporaryItemPool.AddOrUpdate(actorId.Value, set, (b, c) => set);
      return Enumerable.Empty<ItemEntity>();
    }

    public async Task<IEnumerable<ItemEntity>> PickAndRemoveTemporaryItems(Guid? actorId, params Guid[] itemId)
    {
      await Task.CompletedTask;
      if (actorId == null || itemId == null)
      {
        return Enumerable.Empty<ItemEntity>();
      }
      var result = new HashSet<ItemEntity>();
      if (TemporaryItemPool.TryGetValue(actorId.Value, out var items))
      {
        var pickItems = items.Where(b => itemId.Any(b2 => b2 == b.DisplayId)).ToHashSet();
        foreach (var r in pickItems)
        {
          items.Remove(r);
        }
        result = pickItems.Select(b => b.ToEntity()).Where(b => b != null).ToHashSet();
      }
      else
      {
        var set = new HashSet<ItemComponent>();
        TemporaryItemPool.AddOrUpdate(actorId.Value, set, (b, c) => set);
      }
      return result;
    }

    public async Task<ContentResponse<bool>> AddAndIntoTemporary(Guid? actorId, params ItemComponent[] items)
    {
      await Task.CompletedTask;
      var result = new ContentResponse<bool>();
      if (actorId == null)
      {
        return result;
      }
      HashSet<ItemComponent>? set = null;
      if (!TemporaryItemPool.TryGetValue(actorId.Value, out set))
      {
        set = new HashSet<ItemComponent>();
        TemporaryItemPool.AddOrUpdate(actorId.Value, set, (b, c) => set);
      }
      if (items?.Any() != true)
      {
        result.SetSuccess(true);
        return result;
      }
      foreach (var item in items)
      {
        set.Add(item);
      }
      result.SetSuccess(true);
      return result;
    }
  }
}
