using DQQ.Attributes;
using DQQ.Combats;
using DQQ.Enums;
using DQQ.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Skills.Supports
{
	[Pooled]
	public class PiercingSupport : AbSupportSkillProfile
	{
		public override EnumSkillCategory Category => EnumSkillCategory.Strategy;
		public override decimal DamageRate => 0;

		public override EnumSkillNumber ProfileNumber => EnumSkillNumber.PiercingSupport;

		public override string? Name => "穿刺辅助";

		public override string? Discription => "穿刺辅助";

		public override void SetAttackTypeAndArea(IWIthAttackTypeAndArea input)
		{
			input.AttackTypes = EnumAttackType.Piercing;
			input.AreaLevelChange(1);
		}
	}
}
