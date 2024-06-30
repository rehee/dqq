using DQQ.Attributes;
using DQQ.Combats;
using DQQ.Enums;
using DQQ.Strategies.SkillStrategies;
using System.Numerics;

namespace DQQ.Profiles.Mobs
{
	[Pooled]
	public class BigGoblinWeaver : MobNormal
	{
		public override EnumMob ProfileNumber => EnumMob.BigGoblinWeaver;
		public override string? Name => "地精织雾者";
		public override string? Discription => "地精中聪明的家伙, 会一些简单的法术, 皮薄馅大";
		public override double HPPercentage => 0.75;
		public override double DamagePercentage => 1.25;
		public override IEnumerable<MobSkill> Skills => [
			MobSkill.New(EnumSkillNumber.Renew,SkillStrategyDTO.New(
				SkillCastConditionDTO.New(
					EnumStrategyCondition.Players,
					EnumStrategyParty.Contain, 
					null, 
					EnumTarget.Friendly,
					EnumTargetPriority.AnyTarget,
					EnumPropertyCompare.HealthPercentage, 
					EnumCompare.LessThan,1 ))
			),
		];



	}
}
