using DQQ.Attributes;
using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Maps
{
	[Pooled]
	public class Map_1_1 : MapProfile
	{
		public override EnumMapNumber ProfileNumber => EnumMapNumber.Map_1_1;
		public override string? Name => "黑暗的海滩";
		public override string? Discription => "黑暗的海滩";
		public override EnumMob[] MobNumbers => [EnumMob.Crab];
		public override EnumMob[] BossNumbers => [];
		public override int? MaxCombatSecond => 30;
	}
}
