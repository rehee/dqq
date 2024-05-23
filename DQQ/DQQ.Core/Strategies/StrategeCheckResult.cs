using DQQ.Components.Stages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Strategies
{
  public class StrategeCheckResult
  {
    public static StrategeCheckResult New(bool matched, ITarget? matchedTarget)
    {
      return new StrategeCheckResult(matched, matchedTarget);
    }
    public StrategeCheckResult()
    {

    }
    public StrategeCheckResult(bool matched, ITarget? matchedTarget)
    {
      this.Matched = matched;
      this.MatchedTarget = matchedTarget;
    }
    public bool Matched { get; set; }
    public ITarget? MatchedTarget { get; set; }
  }
}
