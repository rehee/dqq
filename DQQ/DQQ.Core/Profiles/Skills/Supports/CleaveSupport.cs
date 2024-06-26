using DQQ.Attributes;
using DQQ.Combats;
using DQQ.Components.Stages.Actors.Characters;
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
	public class CleaveSupport : AbSupportSkillProfile
	{
		public override bool IsAvaliableForCharacter(Character? character)
		{
			return EnumChapter.C_1_6.IsUnlocked(character);
		}
		public override EnumSkillCategory Category => EnumSkillCategory.Strategy;
		public override decimal DamageRate => 0;

		public override EnumSkillNumber ProfileNumber => EnumSkillNumber.CleaveSupport;

		public override string? Name => "顺劈";

		public override string? Discription => "攻击类型改变为顺劈. 攻击当前目标以及额外 随机目标. 数量由外公及属性提升 最多可以额外攻击10个目标";

		public override void SetAttackTypeAndArea(IWIthAttackTypeAndArea input)
		{
			input.AttackTypes = EnumAttackType.Cleave;
			input.ExtraAttackNumber = input.ExtraAttackNumber+4;
		}
	}
}
