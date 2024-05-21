using DQQ.Enums;
using DQQ.Profiles.Durations;
using DQQ.Profiles.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.TickLogs
{
  public class ProfileSource
  {
    public SkillProfile? ProfileFromSkill { get; set; }
    public DurationProfile? ProfileFromDuration { get; set; }
  }
}
