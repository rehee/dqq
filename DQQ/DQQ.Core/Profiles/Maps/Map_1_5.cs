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
	public class Map_1_5 : MapProfile
	{
		public override int? MaxLevel => 50;
		public override int TrashMinMob => 3;
		public override int TrashMaxMob => 3;
		public override EnumChapter? RequesChapter => EnumChapter.C_1_7;
		public override EnumMapNumber ProfileNumber => EnumMapNumber.Map_1_5;
		public override string? Name => "黑暗的海滩 5";
		public override string? Discription => "黑暗的海滩 5";
		public override EnumMob[] MobNumbers => [EnumMob.ShieldCrab,EnumMob.MagicCrab, EnumMob.CrabAssassin];
		public override EnumMob[] BossNumbers => [];
		public override int? MaxCombatSecond => 60;
		public override int TrashWave => 1;
		public override decimal EliteRate => 0.1m;

		protected override List<List<IActor>> GenerateTrashMob(IMap map)
		{
			var result = new List<List<IActor>>();
			var wave = new List<IActor>();
			wave.Add(Monster.Create(EnumMob.ShieldCrab.GetMomster(), map.MapLevel, EliteRate >= RandomHelper.GetRandom(map.TickParameter!.Random, 0, 101) ? Enums.EnumMobRarity.Champion : EnumMobRarity.Normal));
			wave.Add(Monster.Create(EnumMob.MagicCrab.GetMomster(), map.MapLevel, EliteRate >= RandomHelper.GetRandom(map.TickParameter!.Random, 0, 101) ? Enums.EnumMobRarity.Champion : EnumMobRarity.Normal));
			wave.Add(Monster.Create(EnumMob.CrabAssassin.GetMomster(), map.MapLevel, EliteRate >= RandomHelper.GetRandom(map.TickParameter!.Random, 0, 101) ? Enums.EnumMobRarity.Champion : EnumMobRarity.Normal));
			result.Add(wave);
			return result;
		}

	}
}
