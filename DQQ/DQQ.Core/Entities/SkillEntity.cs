using DQQ.Components.Skills;
using DQQ.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace DQQ.Entities
{
  public class SkillEntity : DQQEntityBase<SkillComponent>
  {
    public int Slot { get; set; }
    [ForeignKey(nameof(ActorEntity))]
    public Guid? ActorId { get; set; }
    public virtual ActorEntity? Actor { get; set; }
    public EnumSkill? SkillNumber { get; set; }
  }
}
