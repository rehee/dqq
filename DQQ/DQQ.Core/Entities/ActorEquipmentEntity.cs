using DQQ.Components.Items.Equips;
using DQQ.Components.Stages.Actors;
using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Entities
{
  public class ActorEquipmentEntity : DQQEntityBase<EquipComponent>
  {
    public Guid? ItemId { get; set; }
    public virtual ItemEntity? Equipment { get; set; }

    public Guid? ActorId { get; set; }
    public virtual ActorEntity? Actor { get; set; }

    public EnumEquipSlot Slot { get; set; }
  }
}
