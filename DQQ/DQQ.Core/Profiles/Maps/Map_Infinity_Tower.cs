using DQQ.Attributes;
using DQQ.Components.Stages.Actors;
using DQQ.Components.Stages.Actors.Mobs;
using DQQ.Components.Stages.Maps;
using DQQ.Consts;
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
	public class Map_Infinity_Tower : MapProfile
	{
		public override int TrashMinMob => 1;
		public override int TrashMaxMob => 10;
		public override EnumChapter? RequesChapter => EnumChapter.C_Non_Open;
		public override EnumMapNumber ProfileNumber => EnumMapNumber.Map_Infinity_Tower;
		public override string? Name => "无尽之塔";
		public override string? Discription => "无尽之塔";
		public override EnumMob[] MobNumbers => [];
		public override EnumMob[] BossNumbers => [];
		public override int? MaxCombatSecond => 600;
		public override int TrashWave => 15;
		public override int BossWave => 1;
		public override int? MaxLevel => DQQGeneral.MaxLevel;
	}
}
