using DQQ.Components.Stages.Maps;
using DQQ.Components.Stages;
using DQQ.Strategies.SkillStrategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DQQ.Enums;

namespace DQQ.Helper
{
  public static class StrategyHelper
  {
    public static (bool, ITarget?) MatchSkillStrategy(this IEnumerable<SkillStrategy>? strategies, ITarget? caster, IEnumerable<ITarget>? targets, IMap? map)
    {
      if (strategies?.Any() != true)
      {
        return (true, null);
      }
      foreach (var strategy in strategies.OrderBy(b => b.Priority))
      {
        if (!strategy.Condition)
        {
          return (true, null);
        }
        ITarget? skillTarget = null;
        if (strategy.SkillTarget != null)
        {
          skillTarget = strategy.SkillTarget.SelectTargetByPriority(targets);
        }
        else
        {
          skillTarget = caster?.Target;
        }
        var checkTarget = strategy.CheckTarget == EnumTarget.Self ? caster : skillTarget;
        if (strategy.ConditionPassed(checkTarget))
        {
          return (true, checkTarget);
        }
      }
      return (false, null);
    }

    public static bool ConditionPassed(this SkillStrategy? strategy, ITarget? checkTarget)
    {
      if (strategy == null || checkTarget == null)
      {
        return false;
      }
      switch (strategy.Property)
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
      }
      return false;
    }
    public static ITarget? SelectTargetByPriority(this EnumTargetPriority? priority, IEnumerable<ITarget>? targets)
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
  }
}
