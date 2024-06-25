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
	public class SoftCrab : MobNormal
	{
		public override IEnumerable<MobSkill>? Skills => [MobSkill.New(EnumSkillNumber.NormalAttack)];
		public override EnumMob ProfileNumber => EnumMob.SoftCrab;
		public override string? Name => "软壳蟹";

		public override string? Discription => "壳是软的. 但是力气却是大的";
		public override double DamagePercentage => 0.7;
		public override double HPPercentage => 0.15;
	}
}
