using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Strategies.SkillStrategies
{
	public class SkillStrategyDTO
	{
		public static SkillStrategyDTO Preset(EnumPresetSkillStrategy presetStrategy, int priority = 0)
		{
			return new SkillStrategyDTO()
			{
				PresetStrategy = presetStrategy,
				Priority = priority
			};
		}
		public static SkillStrategyDTO New(SkillCastConditionDTO? castCondition, SkillTargetDTO? skillTarget=null, int priority = 0)
		{
			return new SkillStrategyDTO
			{
				CastCondition = castCondition,
				SkillTarget = skillTarget,
				Priority = priority
			};
		}
		public int Priority { get; set; }
		public EnumPresetSkillStrategy PresetStrategy { get; set; }
		public SkillCastConditionDTO? CastCondition { get; set; }
		public SkillTargetDTO? SkillTarget {  get; set; }
	}
}
