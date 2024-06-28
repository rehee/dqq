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
		public override EnumChapter? UnLockedChapter => EnumChapter.C_1_6;
		public override EnumSkillCategory Category => EnumSkillCategory.Strategy;
		public override decimal DamageRate => 0;

		public override EnumSkillNumber ProfileNumber => EnumSkillNumber.PiercingSupport;

		public override string? Name => "穿刺";

		public override string? Discription => "被辅助的技能可以穿刺. 攻击当前目标并穿透. 继续攻击后排目标. 穿透数目由额外攻击决定. 最多额外穿透10个目标";

		public override void SetAttackTypeAndArea(IWIthAttackTypeAndArea input)
		{
			input.AttackTypes = EnumAttackType.Piercing;
			input.ExtraAttackNumber = input.ExtraAttackNumber + 4;
		}
	}
}
