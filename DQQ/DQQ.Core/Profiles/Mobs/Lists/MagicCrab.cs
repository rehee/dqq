using DQQ.Attributes;
using DQQ.Enums;
using DQQ.Strategies.SkillStrategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Mobs.Lists
{
	[Pooled]
	public class MagicCrab : MobNormal
	{
		public override IEnumerable<MobSkill> Skills =>
			[
			MobSkill.New(EnumSkillNumber.Renew,
				[
				SkillStrategyDTO.New(
				SkillCastConditionDTO.New(
					EnumStrategyCondition.Players,
					EnumStrategyParty.Contain,
					null,
					EnumTarget.Friendly,
					EnumTargetPriority.AnyTarget,
					EnumPropertyCompare.HealthPercentage,
					EnumCompare.LessThan,1 ))
				]),
			MobSkill.New(EnumSkillNumber.Healing,
				[
				SkillStrategyDTO.New(
				SkillCastConditionDTO.New(
					EnumStrategyCondition.Players,
					EnumStrategyParty.Contain,
					null,
					EnumTarget.Friendly,
					EnumTargetPriority.AnyTarget,
					EnumPropertyCompare.HealthPercentage,
					EnumCompare.LessThan,0.5m ))
				]),
			MobSkill.New(EnumSkillNumber.DarkHeal,
				[
				SkillStrategyDTO.New(
				SkillCastConditionDTO.New(
					EnumStrategyCondition.Players,
					EnumStrategyParty.Contain,
					null,
					EnumTarget.Friendly,
					EnumTargetPriority.AnyTarget,
					EnumPropertyCompare.HealthPercentage,
					EnumCompare.LessThan,0.7m ))
				]),
			MobSkill.New(EnumSkillNumber.DarkGreaterHeal,
				[
				SkillStrategyDTO.New(
				SkillCastConditionDTO.New(
					EnumStrategyCondition.Players,
					EnumStrategyParty.Contain,
					null,
					EnumTarget.Friendly,
					EnumTargetPriority.AnyTarget,
					EnumPropertyCompare.HealthPercentage,
					EnumCompare.LessThan,0.8m ))
				])
			];
		public override EnumMob ProfileNumber => EnumMob.MagicCrab;
		public override string? Name => "魔螃蟹";
		public override int QueuePosition => 2;
		public override string? Discription => "闷头加血的家伙";
		public override double DamagePercentage => 3;
		public override double HPPercentage => 1;
		public override bool NotInInfinity => true;
	}
}
