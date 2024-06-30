using DQQ.Attributes;
using DQQ.Components.Stages.Actors;
using DQQ.Components.Stages.Actors.Mobs;
using DQQ.Components.Stages.Maps;
using DQQ.Enums;
using DQQ.Pools;
using DQQ.Strategies.SkillStrategies;
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
	public class CrabKing : MobBoss
	{
		public override EnumMob ProfileNumber => EnumMob.CrabKing;

		public override string? Name => "螃蟹王";
		public override string? Discription => "又大又肥的螃蟹";

		public override double HPPercentage => 40;
		public override double DamagePercentage => 1;
		public override decimal AttackPerSecond => 0.5m;


		public override IEnumerable<MobSkill> Skills =>
		[
			MobSkill.New(EnumSkillNumber.NormalAttack),
			MobSkill.New(EnumSkillNumber.MightySmash,
				[
					SkillStrategyDTO.Preset(EnumPresetSkillStrategy.Target_Enemy_LowLife)
				])
		];

		public override List<IActor> GenerateBossFight(IMap map)
		{
			var result = new List<IActor>();
			result.Add(Monster.Create(this.ProfileNumber.GetMomster()!, map.MapLevel));
			return result;
		}
	}
}
