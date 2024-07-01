using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DQQ.Strategies.SkillStrategies
{
	public class SkillStrategyDTO
	{
		public static SkillStrategyDTO Preset(EnumPresetSkillStrategy presetStrategy, int priority = 0, SkillTargetDTO? skillTarget = null)
		{
			return new SkillStrategyDTO()
			{
				PresetStrategy = presetStrategy,
				Priority = priority,
				UsePreset = true,
				OverrideTarger = skillTarget!=null,
				SkillTarget = skillTarget
			};
		}

		public static SkillStrategyDTO Copy(SkillStrategyDTO dto)
		{
			return JsonSerializer.Deserialize<SkillStrategyDTO>(JsonSerializer.Serialize(dto))?? new SkillStrategyDTO();
		}

		public static SkillStrategyDTO New(SkillCastConditionDTO? castCondition, SkillTargetDTO? skillTarget = null, int priority = 0)
		{
			return new SkillStrategyDTO
			{
				CastCondition = castCondition,
				SkillTarget = skillTarget,
				Priority = priority,
				UsePreset = false,
				OverrideTarger = false
			};
		}
		public Guid Id { get; set; } = Guid.NewGuid();
		public int Priority { get; set; }
		public EnumPresetSkillStrategy PresetStrategy { get; set; }
		public SkillCastConditionDTO? CastCondition { get; set; }
		public SkillTargetDTO? SkillTarget { get; set; }
		public bool UsePreset { get; set; }
		public bool OverrideTarger { get; set; }

	}
}
