using DQQ.Components.Stages.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Entities
{
  public class ActorEntity : DQQEntityBase<Actor>
  {
    public string? MaxHP { get; set; }
    public string? BasicDamage { get; set; }
    public virtual List<SkillEntity>? Skills { get; set; }
    public virtual List<ItemEntity>? Items { get; set; }
    public virtual List<ActorEquipmentEntity>? Equips { get; set; }
  }
}
