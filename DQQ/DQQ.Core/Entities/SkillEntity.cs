using DQQ.Components.Skills;
using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Entities
{
  public class SkillEntity : DQQEntityBase<SkillComponent>
  {
    public int Slot { get; set; }
    public Guid? ActorProfileId { get; set; }
    public virtual ActorEntity? Actor { get; set; }
    public EnumSkill SkillNumber { get; set; }
  }
}
