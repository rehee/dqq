﻿using DQQ.Attributes;
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
	internal class Target_Self_LowLife : PresetStrategyProfile
	{
		public override EnumPresetSkillStrategy ProfileNumber => EnumPresetSkillStrategy.Target_Self_LowLife;
		public override IEnumerable<SkillStrategyDTO> Strategies =>
			[
				SkillStrategyDTO.New(
					SkillCastConditionDTO.New(
						EnumStrategyCondition.Players,
						EnumStrategyParty.Contain,
						null,
						EnumTarget.Self,
						EnumTargetPriority.AnyTarget,
						EnumPropertyCompare.HealthPercentage,
						EnumCompare.LessOrEqual,
						0.3m
						))
			];


	}
}
