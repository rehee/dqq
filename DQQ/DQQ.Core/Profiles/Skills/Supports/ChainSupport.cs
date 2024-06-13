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
	public class ChainSupport : AbSupportSkillProfile
	{
		public override EnumSkillCategory Category => EnumSkillCategory.Strategy;
		public override decimal DamageRate => 0;

		public override EnumSkillNumber ProfileNumber => EnumSkillNumber.ChainSupport;

		public override string? Name => "连锁辅助";

		public override string? Discription => "连锁辅助";

		public override void SetAttackTypeAndArea(IWIthAttackTypeAndArea input)
		{
			input.ExtraAttackNumber = input.ExtraAttackNumber + 2;
			input.AttackTypes = EnumAttackType.Chain;
			input.AreaLevelChange(1);
		}
	}
}
