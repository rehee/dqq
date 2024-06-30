using DQQ.Components.Stages.Maps;
using DQQ.Components.Stages;
using DQQ.Strategies.SkillStrategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DQQ.Enums;
using DQQ.Strategies;
using System.Collections;
using DQQ.Components.Skills;
using DQQ.Consts;
using DQQ.Components.Parameters;

namespace DQQ.Helper
{
	public static class StrategyHelper
	{
		public static StrategeCheckResult MatchSkillStrategy(this IEnumerable<SkillStrategyDTO>? strategies, ComponentTickParameter? parameter, SkillComponent skill)
		{
			if (strategies?.Any() != true)
			{
				return StrategeCheckResult.New(true, null);
			}
			foreach (var strategy in strategies.OrderBy(b => b.Priority))
			{
				var preset = strategy.PresetStrategy.GetPresetStrategy();
				if (preset?.Any() == true)
				{
					var presetResult = preset.MatchSkillStrategy(parameter, skill);
					if (presetResult.Matched)
					{
						return presetResult;
					}
					continue;
				}
				StrategeCheckResult? thisMatch = null;
				var condtionType = strategy?.CastCondition?.ConditionType?? EnumStrategyCondition.NotSpecified;
				switch (condtionType)
				{
					case EnumStrategyCondition.NotSpecified:
						thisMatch = StrategeCheckResult.New(true, null);
						break;
					case EnumStrategyCondition.Target:
						thisMatch = strategy?.CastCondition?.MatchSkillStrategyTarget(parameter?.From, parameter?.EnemyTargets, parameter?.FriendlyTargets, parameter?.Map);
					break;
					case EnumStrategyCondition.Players:
					case EnumStrategyCondition.Enemies:
						thisMatch = strategy?.CastCondition?.MatchSkillStrategyParty(parameter?.From, parameter?.EnemyTargets, parameter?.FriendlyTargets, parameter?.Map);
						break;
					case EnumStrategyCondition.Combat:
					case EnumStrategyCondition.Wave:
						thisMatch = strategy?.CastCondition?.MatchSkillStrategyWave(parameter?.From, parameter?.EnemyTargets, parameter?.Map, skill);
						break;
				}
				if (thisMatch?.Matched == true)
				{
					strategy.SkillTargetOverride(parameter, thisMatch);
					return thisMatch;
				}
			}
			return StrategeCheckResult.New(false, null);
		}


		public static StrategeCheckResult? MatchSkillStrategyTarget(this SkillCastConditionDTO strategy, ITarget? caster, IEnumerable<ITarget>? enemies, IEnumerable<ITarget>? friendlies, IMap? map)
		{
			ITarget? skillTarget = null;

			switch (strategy.ConditionTargetType)
			{
				case EnumTarget.Self:
					skillTarget = caster;
					break;
				case EnumTarget.Target:
					skillTarget = strategy.ConditionTargetPriority.ConditionTargetByPriority(enemies);
					break;
				case EnumTarget.Friendly:
					skillTarget = strategy.ConditionTargetPriority.ConditionTargetByPriority(friendlies);
					break;
			}
			if (strategy.ConditionPassed(skillTarget))
			{
				return StrategeCheckResult.New(true, skillTarget);
			}
			return null;
		}
		public static StrategeCheckResult? MatchSkillStrategyParty(this SkillCastConditionDTO strategy, ITarget? caster, IEnumerable<ITarget>? enemies, IEnumerable<ITarget>? friendlies, IMap? map)
		{
			IEnumerable<ITarget>? checkingTargets = null;
			switch (strategy?.ConditionType)
			{
				case EnumStrategyCondition.Enemies:
					checkingTargets = enemies;
					break;
				case EnumStrategyCondition.Players:
					checkingTargets = friendlies;
					break;
				default:
					return null;
			}

			var aliveTargets = checkingTargets?.Where(b => b.Alive);
			switch (strategy.PartyStrategy)
			{
				case EnumStrategyParty.AliveNumber:
					long aliveNumber = aliveTargets?.Count() ?? 0;
					var checkResult = strategy.Compare.NumberCompare(aliveNumber, strategy.Value);
					if (checkResult == true)
					{
					
						return StrategeCheckResult.New(true,null);
					}
					break;
				case EnumStrategyParty.Contain:

					var containChecks = strategy.OrderByTarget(aliveTargets?.Where(b => strategy.ConditionPassed(b)) ?? []);
					if (containChecks?.Any() == true)
					{
						return StrategeCheckResult.New(true, containChecks?.FirstOrDefault());
					}
					break;
			}
			return null;
		}
		public static StrategeCheckResult? MatchSkillStrategyWave(this SkillCastConditionDTO strategy, ITarget? caster, IEnumerable<ITarget>? targets, IMap? map, SkillComponent skill)
		{
			if (skill == null)
			{
				return null;
			}
			var conditionMatch = false;
			switch (strategy.WaveStrategy)
			{
				case EnumStrategyWave.OnlyBeginning:
					if (strategy.ConditionType == EnumStrategyCondition.Combat)
					{
						conditionMatch = skill.TotalCount == 0;
					}
					if (strategy.ConditionType == EnumStrategyCondition.Wave)
					{
						conditionMatch = skill.WaveCount == 0;
					}
					break;
				case EnumStrategyWave.Period:
					if (strategy?.Value <= 0)
					{
						return null;
					}
					var periodTick = (int)((strategy?.Value ?? 1m) * DQQGeneral.TickPerSecond);
					if (strategy?.ConditionType == EnumStrategyCondition.Combat)
					{
						conditionMatch = map.TickCount % periodTick == 0;
					}
					if (strategy?.ConditionType == EnumStrategyCondition.Wave)
					{
						conditionMatch = map.WaveTickCount % periodTick == 0;
					}
					break;
				case EnumStrategyWave.BossFight:
					conditionMatch = targets?.Any(b => DQQGeneral.GuardianLevel.Contains(b.PowerLevel)) == true;
					break;
				case EnumStrategyWave.EliteFight:
					conditionMatch = targets?.Any(b => DQQGeneral.EliteLevel.Contains(b.PowerLevel) && !DQQGeneral.GuardianLevel.Contains(b.PowerLevel)) == true;
					break;
				case EnumStrategyWave.TrashMob:
					conditionMatch = targets?.All(b => DQQGeneral.TrashLevel.Contains(b.PowerLevel)) == true;
					break;
			}
			if (!conditionMatch)
			{
				return null;
			}

			ITarget? skillTarget = null;

			IEnumerable<ITarget>? checkingTarget = null;
			if (strategy?.ConditionTargetType == EnumTarget.Self)
			{
				checkingTarget = map?.Players;
			}
			else
			{
				checkingTarget = map?.WaveIndex < 0 ? Enumerable.Empty<ITarget>() : map?.MobPool?[map.WaveIndex];
			}
			if (checkingTarget == null)
			{
				
				return StrategeCheckResult.New(true,null);
			}
			var aliveTargets = checkingTarget.Where(b => b.Alive);
			
			return StrategeCheckResult.New(true, strategy.OrderByTarget(aliveTargets)?.FirstOrDefault());
		}
		public static IEnumerable<ITarget>? OrderByTarget(this SkillCastConditionDTO? strategy, IEnumerable<ITarget> targets)
		{
			if (strategy == null || targets == null || strategy?.ConditionTargetPriority == null)
			{
				return targets;
			}
			switch (strategy?.ConditionTargetPriority)
			{
				case EnumTargetPriority.Weakest:
					return targets.OrderBy(b => b.PowerLevel);
				case EnumTargetPriority.Strongest:
					return targets.OrderByDescending(b => b.PowerLevel);
				case EnumTargetPriority.Front:
					return targets;
				case EnumTargetPriority.Back:
					return targets.Reverse();
			}

			return targets;
		}


		public static bool ConditionPassed(this SkillCastConditionDTO? strategy, ITarget? checkTarget)
		{
			if (strategy == null || checkTarget == null)
			{
				return false;
			}
			switch (strategy.PropertyToCheck)
			{
				case EnumPropertyCompare.HealthPercentage:
					return PercentageCompare(strategy.Compare, checkTarget?.PercentageHP, strategy.Value);
				case EnumPropertyCompare.HealthAmount:
					return NumberCompare(strategy.Compare, checkTarget?.CurrentHP, strategy.Value);
			}
			return false;
		}

		public static bool NumberCompare(this EnumCompare? compare, long? checker, decimal? value)
		{
			if (compare == null || checker == null || value == null)
			{
				return false;
			}
			switch (compare)
			{
				case EnumCompare.LessThan:
					return checker < value;
				case EnumCompare.MoreThan:
					return checker > value;
				case EnumCompare.LessOrEqual:
					return checker <= value;
				case EnumCompare.MoreOrEqual:
					return checker >= value;
				case EnumCompare.Equal:
					return checker == value;
				case EnumCompare.NotEqual:
					return checker != value;
			}
			return false;
		}
		public static bool PercentageCompare(this EnumCompare? compare, decimal? checker, decimal? value)
		{
			if (compare == null || checker == null || value == null)
			{
				return false;
			}
			switch (compare)
			{
				case EnumCompare.LessThan:
					return checker < value;
				case EnumCompare.MoreThan:
					return checker > value;
				case EnumCompare.LessOrEqual:
					return checker <= value;
				case EnumCompare.MoreOrEqual:
					return checker >= value;
				case EnumCompare.Equal:
					return checker == value;
				case EnumCompare.NotEqual:
					return checker != value;
			}
			return false;
		}
		public static ITarget? ConditionTargetByPriority(this EnumTargetPriority? priority, IEnumerable<ITarget>? targets)
		{

			if (targets?.Any() != true || priority == null)
			{
				return null;
			}
			var liveTargets = targets.Where(b => b.Alive);
			switch (priority)
			{
				case EnumTargetPriority.AnyTarget:
				case EnumTargetPriority.Front:
					return liveTargets.FirstOrDefault();
				case EnumTargetPriority.Strongest:
					return liveTargets.OrderByDescending(b => b.PowerLevel).FirstOrDefault();
				case EnumTargetPriority.Weakest:
					return liveTargets.OrderBy(b => b.PowerLevel).FirstOrDefault();
				case EnumTargetPriority.Back:
					return liveTargets.LastOrDefault();
			}

			return liveTargets.FirstOrDefault();
		}
		public static void SkillTargetOverride(this SkillStrategyDTO? dto, ComponentTickParameter? parameter, StrategeCheckResult result)
		{
			if (dto?.SkillTarget == null || parameter == null)
			{
				return;
			}

			IEnumerable<ITarget>? targets = null;
			switch (dto?.SkillTarget?.SkillTarget)
			{
				case EnumTarget.Target:
				targets = parameter.EnemyTargets;
				break;
				case EnumTarget.Self:
				targets = parameter.From != null ? [parameter.From!] : [];
				break;
				case EnumTarget.Friendly:
				targets = parameter.FriendlyTargets;
				break;
			}
			result.MatchedTarget = dto?.SkillTarget?.TargetPriority.ConditionTargetByPriority(targets);
		}
	}
}
