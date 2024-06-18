using DQQ.Attributes;
using DQQ.Components.Stages.Actors;
using DQQ.Components.Stages.Actors.Mobs;
using DQQ.Components.Stages.Maps;
using DQQ.Enums;
using DQQ.Pools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DQQ.Profiles.Mobs.BossMobs
{
	[Pooled]
	public class GoblinChief : MobBoss
	{
		public override EnumMob ProfileNumber => EnumMob.GoblinChief;

		public override string? Name => "地精酋长";
		public override string? Discription => "地精中的王者, 总是尝试处决见到的敌人. 变成光杆司令后会发狂.";

		public override double HPPercentage => 5;



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

		public override List<IActor> GenerateBossFight(IMap map)
		{
			var result = new List<IActor>();
			result.Add(Monster.Create(EnumMob.BigGoblin.GetMomster()!, map.MapLevel));
			result.Add(Monster.Create(this.ProfileNumber.GetMomster()!, map.MapLevel));
			result.Add(Monster.Create(EnumMob.BigGoblinWeaver.GetMomster()!, map.MapLevel));
			return result;
		}
	}
}
