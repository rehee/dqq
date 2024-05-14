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
    public async Task<IEnumerable<ItemEntity>> GetAllTemporaryItems(Guid actorId)
    {
      await Task.CompletedTask;
      if (TemporaryItemPool.TryGetValue(actorId, out var items))
      {
        return items.Select(b => b.ToEntity());
      }
      var set = new HashSet<ItemComponent>();
      TemporaryItemPool.AddOrUpdate(actorId, set, (b, c) => set);
      return Enumerable.Empty<ItemEntity>();
    }

    public async Task<ItemEntity?> GetTemporaryItems(Guid actorId, Guid itemId)
    {
      await Task.CompletedTask;
      if (TemporaryItemPool.TryGetValue(actorId, out var items))
      {
        return items.Where(b => b.DisplayId == itemId).Select(b => b.ToEntity()).FirstOrDefault();
      }
      var set = new HashSet<ItemComponent>();
      TemporaryItemPool.AddOrUpdate(actorId, set, (b, c) => set);
      return default(ItemEntity);
    }

    public async Task<ContentResponse<bool>> InsertIntoTemporary(Guid actorId, params ItemComponent[] items)
    {
      await Task.CompletedTask;
      var result = new ContentResponse<bool>();
      HashSet<ItemComponent>? set = null;
      if (!TemporaryItemPool.TryGetValue(actorId, out set))
      {
        set = new HashSet<ItemComponent>();
        TemporaryItemPool.AddOrUpdate(actorId, set, (b, c) => set);
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
