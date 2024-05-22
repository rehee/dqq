using DQQ.Attributes;
using DQQ.Combats;
using DQQ.Components.Items;
using DQQ.Components.Skills;
using DQQ.Enums;
using DQQ.Pools;
using DQQ.Profiles.Items;
using DQQ.Profiles.Items.Equipments;
using Microsoft.Linq.Translations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DQQ.Entities
{
  public class ItemEntity : DQQEntityBase<ItemComponent>, ICombatProperty
  {
    public EnumItem ItemNumber { get; set; }
    [NotMapped]
    public ItemProfile Profile => DQQPool.ItemPool[ItemNumber];

    [ForeignKey(nameof(ActorEntity))]
    public Guid? ActorId { get; set; }
    [JsonIgnore]
    public virtual ActorEntity? Actor { get; set; }

    public int? Quantity { get; set; }
    public int? ItemLevel { get; set; }

    [NotMapped]
    public EquipProfile? EquipProfile => Profile is EquipProfile ? Profile as EquipProfile : null;

    [NotMapped]
    public EnumEquipType? EquipType => EquipProfile?.EquipType;
    public bool IsEquipped => IsEquippedExp.Evaluate(this);
    public static CompiledExpression<ItemEntity, bool> IsEquippedExp =
      DefaultTranslationOf<ItemEntity>.Property(e => e.IsEquipped)
        .Is(e => e.ActorEquipments != null && e.ActorEquipments.Any() == true);
    public virtual List<ActorEquipmentEntity>? ActorEquipments { get; set; }



    




    public decimal? AttackPerSecond { get; set; }
    public decimal? ArmorPercentage { get; set; }
    public decimal? Resistance { get; set; }
    public long? MaximunLife { get; set; }
    public long? Armor { get; set; }
    public long? Damage { get; set; }
    public long? MainHand { get; set; }
    public long? OffHand { get; set; }
    public decimal? DamageModifier { get; set; }
  }
}
