using DQQ.Attributes;
using DQQ.Components.Stages.Actors;
using DQQ.Components.Stages.Actors.Mobs;
using DQQ.Components.Stages.Maps;
using DQQ.Enums;
using DQQ.Helper;
using DQQ.Pools;
using DQQ.Profiles.Mobs;
using DQQ.Profiles.Mobs.Lists;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DQQ.Profiles.Maps
{
	[Pooled]
	public class Chapter_1_3 : MapProfile
	{
		public override int TrashMinMob => 2;
		public override int TrashMaxMob => 3;
		public override EnumChapter? RequesChapter => EnumChapter.C_1_3;
		public override EnumMapNumber ProfileNumber => EnumMapNumber.Chapter_1_3;
		public override string? Name => "黑暗的海滩 2";
		public override string? Discription => "黑暗的海滩 2";
		public override EnumMob[] MobNumbers => [EnumMob.Crab, EnumMob.CrabAssassin];
		public override EnumMob[] BossNumbers => [];
		public override int? MaxCombatSecond => 30;

		public override decimal EliteRate => 0.25m;

		protected override List<List<IActor>> GenerateTrashMob(IMap map)
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

				wave.Add(Monster.Create(EnumMob.Crab.GetMomster(), map.MapLevel, EliteRate >= RandomHelper.GetRandom(map.TickParameter!.Random, 0, 101) ? Enums.EnumMobRarity.Champion : EnumMobRarity.Normal));
				if (mobCount == 3)
				{
					wave.Add(Monster.Create(EnumMob.Crab.GetMomster(), map.MapLevel, EliteRate >= RandomHelper.GetRandom(map.TickParameter!.Random, 0, 101) ? Enums.EnumMobRarity.Champion : EnumMobRarity.Normal));
				}
				wave.Add(Monster.Create(EnumMob.CrabAssassin.GetMomster(), map.MapLevel, EliteRate >= RandomHelper.GetRandom(map.TickParameter!.Random, 0, 101) ? Enums.EnumMobRarity.Champion : EnumMobRarity.Normal));

				result.Add(wave.OrderBy(b => b.MonstetProfile?.QueuePosition ?? 0).Select(b => b as IActor).ToList());
			}
			return result;
		}
	}
}
