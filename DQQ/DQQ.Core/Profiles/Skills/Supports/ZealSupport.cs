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
	public class ZealSupport : AbSupportSkillProfile
	{
		public override decimal DamageRate => 0;

		public override EnumSkillNumber ProfileNumber => EnumSkillNumber.ZealSupport;

		public override string? Name => "狂热辅助";

		public override string? Discription => "狂热辅助";

		public override void SetAttackTypeAndArea(IWIthAttackTypeAndArea input)
		{
			input.ExtraAttackNumber = input.ExtraAttackNumber + 2;
			input.AttackTypes = EnumAttackType.MultiAttack;
			input.AreaLevelChange(1);
		}
	}
}
