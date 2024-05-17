using DQQ.Components.Stages.Actors;
using DQQ.Entities.ChangeHandlers;
using DQQ.Enums;
using ReheeCmf.Commons;
using ReheeCmf.Components.ChangeComponents;
using ReheeCmf.Handlers.EntityChangeHandlers;
using ReheeCmf.Helpers;
using System.ComponentModel.DataAnnotations;

namespace DQQ.Entities
{
  public class ActorEntity : DQQEntityBase<Actor>
  {
    public string? OwnerId { get; set; }
    public Int64? MaxHP { get; set; }
    public Int64? BasicDamage { get; set; }
    public EnumTargetPriority? TargetPriority { get; set; }
    
    public virtual List<SkillEntity>? Skills { get; set; }
    public virtual List<ItemEntity>? Items { get; set; }
    public virtual List<ActorEquipmentEntity>? Equips { get; set; }

    [Timestamp]
    public byte[]? Version { get; set; }
  }

  [EntityChangeTracker<ActorEntity>]

  public class ActorEntityHandler : BaseHandler<ActorEntity>
  {
    public override async Task<IEnumerable<ValidationResult>> ValidationAsync(CancellationToken ct = default)
    {
      var baseResult = await base.ValidationAsync(ct);
      var current = new List<ValidationResult>();

      if (String.IsNullOrEmpty(entity.Name))
      {
        current.Add(ValidationResultHelper.New("Name is required", nameof(entity.Name)));
        goto ToReturn;
      }

      var sameActor = await query.FirstOrDefaultAsync(context.Query<ActorEntity>(true)
        .Where(b => b.Name == entity.Name && b.Id != entity.Id), ct);
      if (sameActor != null)
      {
        current.Add(ValidationResultHelper.New("Name already used", nameof(entity.Name)));
        goto ToReturn;
      }

    ToReturn:
      return current.Concat(baseResult);
    }

    public override async Task BeforeCreateAsync(CancellationToken ct = default)
    {
      await base.BeforeCreateAsync(ct);
      var existngs = await query.ToArrayAsync(context.Query<ActorEntity>(true).Where(b => b.OwnerId == UserId).Select(b => b.Id), ct);
      if (existngs?.Count() >= 3)
      {
        StatusException.Throw(System.Net.HttpStatusCode.BadRequest, "already with 3 chars");
      }
    }
  }
}
