using DQQ.Components.Items;
using DQQ.Components.Skills;
using DQQ.Enums;
using DQQ.Pools;
using DQQ.Profiles.Items;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Entities
{
  public class ItemEntity : DQQEntityBase<ItemComponent>
  {
    public EnumItem ItemNumber { get; set; }
    [NotMapped]
    public ItemProfile Profile => DQQPool.ItemPool[ItemNumber];

    public Guid? OwnerId { get; set; }
    public virtual ActorEntity? Owner { get; set; }

    public int? Quantity { get; set; }
    public int? ItemLevel { get; set; }



  }
}
