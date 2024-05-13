using DQQ.Components.Items;
using DQQ.Components.Items.Equips;
using DQQ.Components.Stages.Actors;
using DQQ.Enums;
using DQQ.Helper;
using DQQ.Profiles.Items.Equipments;
using Microsoft.Extensions.DependencyInjection;
using ReheeCmf.Components.ChangeComponents;
using ReheeCmf.Handlers.EntityChangeHandlers;
using ReheeCmf.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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


  [EntityChangeTracker<ActorEquipmentEntity>]

  public class ActorEquipmentEntityHandler : EntityChangeHandler<ActorEquipmentEntity>
  {
    protected IAsyncQuery? query => sp?.GetService<IAsyncQuery>() ?? null;

    public override Task BeforeCreateAsync(CancellationToken ct = default)
    {
      return base.BeforeCreateAsync(ct);
    }
    public override async Task<IEnumerable<ValidationResult>> ValidationAsync(CancellationToken ct = default)
    {
      var result = new List<ValidationResult>();
      var baseResult = await base.ValidationAsync(ct);
      if (entity.EquipSlot == null)
      {
        result.Add(new ValidationResult("You need a slot to equip", new[] { nameof(entity.EquipSlot) }));
      }
      else
      {
        var avaliableSlot = entity.Item.EquipType.GetAvaliableSlots();
        if (avaliableSlot?.Any() != true)
        {
          result.Add(new ValidationResult("Not equip", new[] { nameof(entity.ItemId) }));
        }
        else
        {
          if (avaliableSlot.Contains(entity.EquipSlot.Value) != true)
          {
            result.Add(new ValidationResult("Not avaliable slot", new[] { nameof(entity.ItemId) }));
          }
        }
      }
      if (entity.ActorId == null || entity.Item.ActorId == null || entity.Item.ActorId != entity.ActorId)
      {
        result.Add(new ValidationResult("You cant equip that item", new[] { nameof(entity.ActorId) }));
      }
      if (entity.Item == null || entity.Item.EquipType == null)
      {
        result.Add(new ValidationResult("Item is not an equip", new[] { nameof(entity.ItemId) }));
      }
      if (entity.ActorId == null)
      {
        result.Add(new ValidationResult("ActorId is Empty", new[] { nameof(entity.ActorId) }));
      }

      if (result?.Any() != true)
      {
        var existing = await query.ToArrayAsync(context.Query<ActorEquipmentEntity>(true)
        .Where(b => b.ActorId == entity.ActorId && b.EquipSlot == entity.EquipSlot && b.Id != entity.Id)
        .Select(b => 1), ct);
        if (existing?.Any() == true)
        {
          result.Add(new ValidationResult("Equipslot is already for user", new[] { nameof(entity.EquipSlot) }));
        }
      }
      return baseResult.Concat(result);
    }
  }
}
