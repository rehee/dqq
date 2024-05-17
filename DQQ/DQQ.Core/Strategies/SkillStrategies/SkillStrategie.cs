using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Strategies.SkillStrategies
{
  public class SkillStrategy
  {
    public int Priority { get; set; }
    public bool Condition { get; set; }
    public EnumTargetPriority? SkillTarget { get; set; }

    public EnumTarget? CheckTarget { get; set; }
    public EnumPropertyCompare? Property { get; set; }
    public EnumCompare? Compare { get; set; }
    public decimal? Value { get; set; }
  }
}
