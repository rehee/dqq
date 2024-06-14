using DQQ.Attributes;
using DQQ.Combats;
using DQQ.Enums;
using DQQ.Strategies.SkillStrategies;
using System.Numerics;

namespace DQQ.Profiles.Mobs
{
	[Pooled]
	public class BigGoblinWeaver : NormalMob
	{
		public override EnumMob ProfileNumber => EnumMob.BigGoblinWeaver;
		public override string? Name => "地精织雾者";
		public override string? Discription => "";
		public override Int64 Damage => 3;
		public override Int64 HP => 25;
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

		

		public override decimal DropRate => 1m;

		public override Int64 XP => 3;
	}
}
