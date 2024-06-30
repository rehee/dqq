using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Strategies.SkillStrategies
{
	public class SkillCastConditionDTO
	{
		public static SkillCastConditionDTO New(
		EnumStrategyCondition conditionType ,
		EnumStrategyParty? partyStrategy,
		EnumStrategyWave? waveStrategy,
		EnumTarget? conditionTargetType,
		EnumTargetPriority? conditionTargetPriority,
		
		EnumPropertyCompare? propertyToCheck,
		EnumCompare? compare,
		decimal? value
			)
		{
			return new SkillCastConditionDTO
			{
				ConditionType = conditionType,
				PartyStrategy = partyStrategy,
				WaveStrategy = waveStrategy,
				ConditionTargetPriority = conditionTargetPriority,
				ConditionTargetType = conditionTargetType,
				PropertyToCheck = propertyToCheck,
				Compare = compare,
				Value = value
			};
		}
		public EnumStrategyCondition ConditionType { get; set; }
		public EnumStrategyParty? PartyStrategy { get; set; }
		public EnumStrategyWave? WaveStrategy { get; set; }
		public EnumTargetPriority? ConditionTargetPriority { get; set; }
		public EnumTarget? ConditionTargetType { get; set; }
		public EnumPropertyCompare? PropertyToCheck { get; set; }
		public EnumCompare? Compare { get; set; }
		public decimal? Value { get; set; }
	}
}
