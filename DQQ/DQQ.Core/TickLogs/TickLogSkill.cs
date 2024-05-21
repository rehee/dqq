using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.TickLogs
{
  public class TickLogSkill
  {
    public string? SkillName { get; set; }
    public EnumSkill? SkillNumber { get; set; }
    public EnumDurationNumber? DurationNumber { get; set; }
  }
}
