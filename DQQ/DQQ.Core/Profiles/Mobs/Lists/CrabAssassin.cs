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
	public class CrabAssassin : MobNormal
	{
		public override IEnumerable<MobSkill>? Skills => [MobSkill.New(EnumSkillNumber.NormalAttack)];
		public override EnumMob ProfileNumber => EnumMob.CrabAssassin;
		public override string? Name => "螃蟹刺客";
		public override int QueuePosition => 30;
		public override string? Discription => "功高防低的螃蟹,一棒子就可以打碎. 但千万别被他夹到. 很疼. 倾向呆在队伍的后方偷袭你";
		public override double DamagePercentage => 0.7;
		public override double HPPercentage => 0.05;
		public override decimal AttackSpeedModify => -0.90m;
	}
}
