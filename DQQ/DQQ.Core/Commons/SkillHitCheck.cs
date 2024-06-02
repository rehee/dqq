using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Commons
{
  public class SkillHitCheck
  {
    public EnumHitCheck? HitCheck { get; set; }
    public EnumHitCheck[]? IgnoreCheck { get; set; }
  }
}
