using DQQ.Attributes;
using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Mobs.Lists
{
	[Pooled]
	public class Crab : MobNormal
	{
		public override IEnumerable<MobSkill>? Skills => [MobSkill.New(EnumSkillNumber.NormalAttack)];
		public override EnumMob ProfileNumber => EnumMob.Crab;
		public override string? Name => "螃蟹";

		public override string? Discription => "平平无奇的螃蟹. 一眼看上去还有点弱";
		public override double DamagePercentage => 0.5;
		public override double HPPercentage => 0.5;
	}
}
