using DQQ.Commons.DTOs;
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
		public async Task<IMap?> CreateMap(IDQQComponent? creator, int? mapTier, int? mapSubTier)
		{
			if (creator == null || creator.DisplayId == null)
			{
				return null;
			}

			var hasMap = mapPool.TryGetValue(creator.DisplayId.Value, out var map);
			T? newMap = null;
			var dto = new CombatRequestDTO
			{
				Creator = creator,
				ActorId = creator.DisplayId,
				MapLevel = mapTier ?? 0,
				MapNumber = Enums.EnumMapNumber.Chapter_1_1,
				RandomGuid = Guid.NewGuid(),
				SubMapLevel = mapSubTier ?? 0,
			};

			if (!hasMap)
			{
				newMap = new T();
				

				await newMap.Initialize(dto);
				var added = mapPool.TryAdd(creator.DisplayId.Value, newMap);
				return added ? newMap : null;
			}
			newMap = new T();
			await newMap.Initialize(dto);
			mapPool[creator.DisplayId.Value] = newMap;
			return newMap;
		}
	}
}
