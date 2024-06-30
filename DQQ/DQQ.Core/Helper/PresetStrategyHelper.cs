using DQQ.Enums;
using DQQ.Pools;
using DQQ.Profiles.PresetStrategies;
using DQQ.Strategies.SkillStrategies;

namespace DQQ.Helper
{
	public static class PresetStrategyHelper
	{
		public static IEnumerable<SkillStrategyDTO> GetPresetStrategy(this EnumPresetSkillStrategy strategy)
		{
			var profile = DQQPool.TryGet<PresetStrategyProfile, EnumPresetSkillStrategy>(strategy);
			if (profile != null)
			{
				return profile.Strategies;
			}
			return Enumerable.Empty<SkillStrategyDTO>();
		}


	}
}
