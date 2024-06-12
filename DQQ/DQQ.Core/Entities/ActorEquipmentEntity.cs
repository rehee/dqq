using DQQ.Components.Items.Equips;
using DQQ.Enums;
using DQQ.Helper;
using ReheeCmf.Components.ChangeComponents;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DQQ.Entities
{
	public class ActorEquipmentEntity : DQQEntityBase<EquipComponent>
  {
    [ForeignKey(nameof(ItemEntity))]
    public Guid? ItemId { get; set; }
    [JsonIgnore]
    public virtual ItemEntity? Item { get; set; }

    [ForeignKey(nameof(ActorEntity))]
    public Guid? ActorId { get; set; }
    [JsonIgnore]
    public virtual ActorEntity? Actor { get; set; }
    public EnumEquipSlot? EquipSlot { get; set; }
  }


  
}
