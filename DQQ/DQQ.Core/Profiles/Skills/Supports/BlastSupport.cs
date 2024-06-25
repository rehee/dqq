using DQQ.Attributes;
using DQQ.Combats;
using DQQ.Components.Stages.Actors.Characters;
using DQQ.Enums;
using DQQ.Helper;

namespace DQQ.Profiles.Skills.Supports
{
	[Pooled]
	public class BlastSupport : AbSupportSkillProfile
	{
		public override bool IsAvaliableForCharacter(Character? character)
		{
			return EnumChapter.C_1_6.IsUnlocked(character);
		}
		public override decimal DamageRate => 0;
		public override EnumSkillCategory Category => EnumSkillCategory.Strategy;
		public override EnumSkillNumber ProfileNumber => EnumSkillNumber.BlastSupport;

		public override string? Name => "爆裂";

		public override string? Discription => "攻击类型改变为 范围. 且范围等级上升";

		public override void SetAttackTypeAndArea(IWIthAttackTypeAndArea input)
		{
			input.AttackTypes = EnumAttackType.Area;
			input.AreaLevelChange(2);
		}
	}
}
