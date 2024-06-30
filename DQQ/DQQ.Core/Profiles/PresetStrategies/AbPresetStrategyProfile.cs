using DQQ.Enums;
using DQQ.Strategies.SkillStrategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.PresetStrategies
{
	public abstract class PresetStrategyProfile : DQQProfile<EnumPresetSkillStrategy>
	{
		public override string? Name => ProfileNumber.ToString();
		public override string? Discription => ProfileNumber.ToString();
		public abstract IEnumerable<SkillStrategyDTO> Strategies { get; }

	}
}
