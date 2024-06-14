using DQQ.Attributes;
using DQQ.Combats;
using DQQ.Enums;
using DQQ.Strategies.SkillStrategies;
using System.Numerics;

namespace DQQ.Profiles.Mobs
{
	[Pooled]
	public class BigGoblinWeaver : AbMobNormal
	{
		public override EnumMob ProfileNumber => EnumMob.BigGoblinWeaver;
		public override string? Name => "地精织雾者";
		public override string? Discription => "地精中聪明的家伙, 会一些简单的法术, 皮薄馅大";
		public override double HPPercentage => 0.75;
		public override double DamagePercentage => 1.25;
		public override IEnumerable<MobSkill>? Skills => [
			MobSkill.New(EnumSkillNumber.Renew,new SkillStrategy
			{
				Condition = EnumStrategyCondition.Target,
				CheckTarget = EnumTarget.Friendly,
				SkillTarget = EnumTargetPriority.Strongest,
				Compare = EnumCompare.LessThan,
				Value = 1.1m,
				PartyStrategy = EnumStrategyParty.Contain,
				Property = EnumPropertyCompare.HealthPercentage,
			}),
			//MobSkill.New(EnumSkillNumber.Rend)
		];



	}
}
