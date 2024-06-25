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
	public class ZealSupport : AbSupportSkillProfile
	{
		public override bool IsAvaliableForCharacter(Character? character)
		{
			return EnumChapter.C_1_6.IsUnlocked(character);
		}
		public override EnumSkillCategory Category => EnumSkillCategory.Strategy;
		public override decimal DamageRate => 0;

		public override EnumSkillNumber ProfileNumber => EnumSkillNumber.ZealSupport;

		public override string? Name => "狂热";

		public override string? Discription => "将技能类型改为多重 并且可以额外攻击4次";

		public override void SetAttackTypeAndArea(IWIthAttackTypeAndArea input)
		{
			input.ExtraAttackNumber = input.ExtraAttackNumber + 4;
			input.AttackTypes = EnumAttackType.MultiAttack;
			input.AreaLevelChange(1);
		}
	}
}
