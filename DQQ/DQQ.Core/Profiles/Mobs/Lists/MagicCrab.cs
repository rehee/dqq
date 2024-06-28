using DQQ.Attributes;
using DQQ.Enums;
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
				new Strategies.SkillStrategies.SkillStrategy
				{
					Condition = EnumStrategyCondition.Players,
					PartyStrategy = EnumStrategyParty.Contain,
					Property = EnumPropertyCompare.HealthPercentage,
					Value = 1,
					Compare = EnumCompare.LessThan,
					CheckTarget = EnumTarget.Friendly,
					SkillTarget = EnumTargetPriority.Front,
				}]
				),
			MobSkill.New(EnumSkillNumber.Healing,
				[
				new Strategies.SkillStrategies.SkillStrategy
				{
					Condition = EnumStrategyCondition.Players,
					PartyStrategy = EnumStrategyParty.Contain,
					Property = EnumPropertyCompare.HealthPercentage,
					Value = 0.6m,
					Compare = EnumCompare.LessThan,
					CheckTarget = EnumTarget.Friendly,
					SkillTarget = EnumTargetPriority.Front,
				}]
				),
			MobSkill.New(EnumSkillNumber.DarkHeal,
				[
				new Strategies.SkillStrategies.SkillStrategy
				{
					Condition = EnumStrategyCondition.Players,
					PartyStrategy = EnumStrategyParty.Contain,
					Property = EnumPropertyCompare.HealthPercentage,
					Value = 0.85m,
					Compare = EnumCompare.LessThan,
					CheckTarget = EnumTarget.Friendly,
					SkillTarget = EnumTargetPriority.Back,
				}]
				),
			MobSkill.New(EnumSkillNumber.DarkGreaterHeal,
				[
				new Strategies.SkillStrategies.SkillStrategy
				{
					Condition = EnumStrategyCondition.Players,
					PartyStrategy = EnumStrategyParty.Contain,
					Property = EnumPropertyCompare.HealthPercentage,
					Value = 0.85m,
					Compare = EnumCompare.LessThan,
					CheckTarget = EnumTarget.Friendly,
					SkillTarget = EnumTargetPriority.Front,
				}]
				)
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
