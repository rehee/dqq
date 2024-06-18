using DQQ.Attributes;
using DQQ.Enums;
using System.Numerics;

namespace DQQ.Profiles.Mobs
{
	[Pooled]
	public class Goblin : MobNormal
	{
		public override EnumMob ProfileNumber => EnumMob.Goblin;
		public override string? Name => "地精";
		public override string? Discription => "随处可见的地精平平无奇";
		public override IEnumerable<MobSkill>? Skills => new[] { MobSkill.New(EnumSkillNumber.NormalAttack) };

	}
}
