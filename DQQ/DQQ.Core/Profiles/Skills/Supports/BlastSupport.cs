using DQQ.Attributes;
using DQQ.Combats;
using DQQ.Enums;
using DQQ.Helper;

namespace DQQ.Profiles.Skills.Supports
{
	[Pooled]
	public class BlastSupport : AbSupportSkillProfile
	{
		public override decimal DamageRate => 0;
		public override EnumSkillCategory Category => EnumSkillCategory.Strategy;
		public override EnumSkillNumber ProfileNumber => EnumSkillNumber.BlastSupport;

		public override string? Name => "爆裂辅助";

		public override string? Discription => "攻击类型改变为AOE. 且aoe等级上升";

		public override void SetAttackTypeAndArea(IWIthAttackTypeAndArea input)
		{
			input.AttackTypes = EnumAttackType.Area;
			input.AreaLevelChange(1);
		}
	}
}
