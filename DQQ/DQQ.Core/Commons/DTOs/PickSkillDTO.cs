using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Commons.DTOs
{
  public class PickSkillDTO
  {
    public Guid? ActorId { get; set; }
    public EnumSkill? SkillNumber { get; set; }
    public int? Slot { get; set; }
  }
}
