using DQQ.Components.Stages.Actors.Characters;
using DQQ.Pools;
using DQQ.Profiles.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Helper
{
	public static class MapHelper
	{
		public static MapProfile[] GetUnlockedMaps(this Character character)
		{
			var allMaps = DQQPool.MapPools.Select(b => b.Value);
			return allMaps.Where(b => b.RequestLevel <= character.Level)
				.Where(b => b.RequesChapter == null || (int)b.RequesChapter <= (int)character.Chapter)
				.Where(b => b.RequestMap == null || (int)b.RequestMap <= (int)character.Map)
				.ToArray();
		}
	}
}
