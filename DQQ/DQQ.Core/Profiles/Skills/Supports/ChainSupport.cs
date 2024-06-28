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
	public class ChainSupport : AbSupportSkillProfile
	{
		public override EnumChapter? UnLockedChapter => EnumChapter.C_1_6;
		public override EnumSkillCategory Category => EnumSkillCategory.Strategy;
		public override decimal DamageRate => 0;

		public override EnumSkillNumber ProfileNumber => EnumSkillNumber.ChainSupport;

		public override string? Name => "连锁";

		public override string? Discription => "将技能的攻击方式改变为连锁, 并且可以额外集中5次";

		public override void SetAttackTypeAndArea(IWIthAttackTypeAndArea input)
		{
			input.ExtraAttackNumber = input.ExtraAttackNumber + 5;
			input.AttackTypes = EnumAttackType.Chain;
			input.AreaLevelChange(1);
		}
	}
}
