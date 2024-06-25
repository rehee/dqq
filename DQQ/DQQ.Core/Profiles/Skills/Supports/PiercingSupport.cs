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
	public class PiercingSupport : AbSupportSkillProfile
	{
		public override bool IsAvaliableForCharacter(Character? character)
		{
			return EnumChapter.C_1_6.IsUnlocked(character);
		}
		public override EnumSkillCategory Category => EnumSkillCategory.Strategy;
		public override decimal DamageRate => 0;

		public override EnumSkillNumber ProfileNumber => EnumSkillNumber.PiercingSupport;

		public override string? Name => "穿刺";

		public override string? Discription => "被辅助的技能可以穿刺. 且将范围提升2级";

		public override void SetAttackTypeAndArea(IWIthAttackTypeAndArea input)
		{
			input.AttackTypes = EnumAttackType.Piercing;
			input.AreaLevelChange(2);
		}
	}
}
