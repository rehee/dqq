using DQQ.Components.Stages.Actors;
using DQQ.Components.Stages.Actors.Mobs;
using DQQ.Components.Stages.Maps;
using DQQ.Enums;
using DQQ.Helper;
using DQQ.Pools;
using DQQ.Profiles.Mobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Maps
{
	public abstract class MapProfile : DQQProfile<EnumMapNumber>
	{
		public virtual EnumChapter? RequesChapter => null;
		public virtual EnumMapNumber? RequestMap => null;
		public abstract int? MaxCombatSecond { get; }
		public virtual int RequestLevel => 0;
		public virtual int? MaxLevel => null;
		public virtual int TrashWave => 1;
		public virtual int TrashMinMob => 1;
		public virtual int TrashMaxMob => 1;
		public virtual int BossWave => 0;
		public virtual decimal EliteRate => 0;
		public abstract EnumMob[] MobNumbers { get; }
		public abstract EnumMob[] BossNumbers { get; }
		public virtual List<List<IActor>>? GenerateMonster(IMap map)
		{
			return GenerateTrashMob(map).Concat(GenerateBossMob(map)).ToList();
		}
		protected virtual List<List<IActor>> GenerateTrashMob(IMap map)
		{
			if (MobNumbers.Length <= 0)
			{
				return [];
			}
			var result = new List<List<IActor>>();
			for (var i = 0; i < TrashWave; i++)
			{
				var wave = new List<Monster>();
				var mobCount = RandomHelper.GetRandomInt(map.TickParameter!.Random, TrashMinMob, TrashMaxMob);

				for (var im = 0; im < mobCount; im++)
				{

					var monsterNumber = MobNumbers.GetRamdom(map.TickParameter!.Random);
					var monster = DQQPool.TryGet<MobProfile, EnumMob>(monsterNumber);
					if (monster == null)
					{
						continue;
					}
					var isElite = EliteRate >= RandomHelper.GetRandom(map.TickParameter!.Random, 0, 101);
					wave.Add(Monster.Create(monster, map.MapLevel, isElite ? Enums.EnumMobRarity.Champion : EnumMobRarity.Normal));
				}
				result.Add(wave.OrderBy(b => b.MonstetProfile?.QueuePosition ?? 0).Select(b => b as IActor).ToList());
			}
			return result;
		}

		protected virtual List<List<IActor>> GenerateBossMob(IMap map)
		{
			if (BossNumbers.Length <= 0)
			{
				return [];
			}
			var result = new List<List<IActor>>();
			for (var i = 0; i < BossWave; i++)
			{
				var monster = BossNumbers.GetRamdom(map.TickParameter!.Random);
				var boss = DQQPool.TryGet<MobProfile, EnumMob>(monster);
				if (boss == null)
				{
					continue;
				}
				if (boss is MobBoss bossMob)
				{
					result.Add(bossMob.GenerateBossFight(map));
				}
			}
			return result;
		}
	}
}
