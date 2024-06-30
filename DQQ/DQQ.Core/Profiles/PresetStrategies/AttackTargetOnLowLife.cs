using DQQ.Attributes;
using DQQ.Enums;
using DQQ.Strategies.SkillStrategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.PresetStrategies
{
	[Pooled]
	internal class AttackTargetOnLowLife : PresetStrategyProfile
	{
		public override EnumPresetSkillStrategy ProfileNumber => EnumPresetSkillStrategy.AttackTargetOnLowLife;
		public override IEnumerable<SkillStrategyDTO> Strategies => 
			[
				SkillStrategyDTO.New(SkillCastConditionDTO.New(
					EnumStrategyCondition.Enemies, 
					EnumStrategyParty.Contain,
					null,
					EnumTarget.Target,
					EnumTargetPriority.AnyTarget,
					EnumPropertyCompare.HealthPercentage,
					EnumCompare.LessOrEqual,
					0.3m
					))

			];

		
	}
}
