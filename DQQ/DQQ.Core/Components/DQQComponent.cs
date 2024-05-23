using DQQ.Entities;
using DQQ.Profiles;
using System.Text.Json.Serialization;

namespace DQQ.Components
{
  public abstract class DQQComponent : IDQQComponent
  {
    public virtual Guid? DisplayId { get; set; }
    public virtual string? DisplayName { get; set; }

    [JsonIgnore]
    public IDQQEntity? Entity { get; set; }
    [JsonIgnore]
    public IDQQProfile? Profile { get; set; }

    public virtual void Initialize(IDQQEntity entity)
    {
      Entity = entity;
      DisplayId = entity.Id;
      DisplayName = entity.Name;
    }
  }
}
