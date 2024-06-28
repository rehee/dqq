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
	public class ShieldCrab : MobNormal
	{
		public override IEnumerable<MobSkill> Skills =>
			[
				MobSkill.New(EnumSkillNumber.NormalAttack),
				MobSkill.New(EnumSkillNumber.SacrificialGuard)
			];
		public override EnumMob ProfileNumber => EnumMob.ShieldCrab;
		public override string? Name => "盾螃蟹";
		public override int QueuePosition => 0;
		public override string? Discription => "以盾为名,挺硬的";
		public override double DamagePercentage => 0.7;
		public override double HPPercentage => 5;
		public override decimal AttackPerSecond =>0.5m;
	}
}
