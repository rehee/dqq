using DQQ.Attributes;
using DQQ.Enums;
using System.Numerics;

namespace DQQ.Profiles.Mobs
{
	[Pooled]
	public class BigGoblin : AbMobNormal
	{
		public override EnumMob ProfileNumber => EnumMob.BigGoblin;
		public override string? Name => "大地精";
		public override string? Discription => "更大 更强壮的地精. 皮糙肉厚";

		public override double HPPercentage => 1.25;
		public override double DamagePercentage => 0.75;

		public override IEnumerable<MobSkill>? Skills => new[] { MobSkill.New(EnumSkillNumber.NormalAttack) };


	}
}
