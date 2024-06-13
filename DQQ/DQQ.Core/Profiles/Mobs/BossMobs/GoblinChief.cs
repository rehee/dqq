using DQQ.Attributes;
using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Mobs.BossMobs
{
	[Pooled]
	public class GoblinChief : BossMob
	{
		public override long Damage => 5;

		public override long HP => 50;

		public override IEnumerable<MobSkill>? Skills => new[]
		{
			MobSkill.New(EnumSkillNumber.NormalAttack),
			MobSkill.New(EnumSkillNumber.Renew),
			MobSkill.New(EnumSkillNumber.HatefulStrike,
				new Strategies.SkillStrategies.SkillStrategy
				{
					Priority=0,
					Condition = EnumStrategyCondition.Target,
					CheckTarget = EnumTarget.Target,
					Property= EnumPropertyCompare.HealthPercentage,
					Compare= EnumCompare.LessThan,
					Value = 0.25m,
				},
				new Strategies.SkillStrategies.SkillStrategy
				{
					Priority=0,
					Condition = EnumStrategyCondition.Players,
					PartyStrategy = EnumStrategyParty.AliveNumber,
					Property= EnumPropertyCompare.HealthPercentage,
					Compare= EnumCompare.LessOrEqual,
					Value = 1m,
				}
				)
		};

		public override decimal DropRate => 0.15m;

		public override long XP => 20;

		public override EnumMob ProfileNumber => EnumMob.GoblinChief;

		public override string? Name => "地精酋长";

		public override string? Discription => "地精酋长";
	}
}
