using DQQ.Components;
using DQQ.Components.Stages.Maps;

namespace DQQ.Services.MapServices
{
  public class MapService<T> : IMapService where T : class, IMap, new()
  {
    private static Dictionary<Guid, IMap> mapPool = new Dictionary<Guid, IMap>();
    public MapService()
    {

    }
    public async Task<IMap?> CreateMap(IDQQComponent creator, int mapTier, int mapSubTier)
    {
      if (creator == null || creator.DisplayId == null)
      {
        return null;
      }
      var hasMap = mapPool.TryGetValue(creator.DisplayId.Value, out var map);
      T? newMap = null;
      if (!hasMap)
      {
        newMap = new T();
        await newMap.Initialize(creator, mapTier, mapSubTier);
        var added = mapPool.TryAdd(creator.DisplayId.Value, newMap);
        return added ? newMap : null;
      }
      newMap = new T();
      await newMap.Initialize(creator, mapTier, mapSubTier);
      mapPool[creator.DisplayId.Value] = newMap;
      return newMap;
    }
  }
}
