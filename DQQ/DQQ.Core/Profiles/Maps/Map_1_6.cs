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
	public class Map_1_6 : MapProfile
	{
		public override int TrashMinMob => 3;
		public override int TrashMaxMob => 3;
		public override EnumChapter? RequesChapter => EnumChapter.C_1_8;
		public override EnumMapNumber ProfileNumber => EnumMapNumber.Map_1_6;
		public override string? Name => "黑暗的海滩 6";
		public override string? Discription => "黑暗的海滩 6";
		public override EnumMob[] MobNumbers => [];
		public override EnumMob[] BossNumbers => [EnumMob.CrabKing];
		public override int? MaxCombatSecond => 60;
		public override int TrashWave => 0;
		public override int BossWave => 1;

	}
}
