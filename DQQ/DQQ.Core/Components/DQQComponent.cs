using DQQ.Components.Stages.Maps;
using DQQ.Components.Stages;
using DQQ.Entities;
using DQQ.Profiles;
using ReheeCmf.Responses;
using System.Text.Json.Serialization;
using ReheeCmf.Helpers;
using DQQ.Components.Parameters;

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

    public bool IsDisposed { get; protected set; }
    public virtual void Dispose()
    {
      if (IsDisposed)
      {
        return;
      }
      IsDisposed = true;
      Entity = null;
      Profile = null;
    }

    public virtual void Initialize(IDQQEntity entity)
    {
      Entity = entity;
      DisplayId = entity.Id;
      DisplayName = entity.Name;
    }
    public virtual async Task<ContentResponse<bool>> OnTick(ComponentTickParameter parameter)
    {
      var result = new ContentResponse<bool>();
      result.SetSuccess(parameter.From?.Alive == true);
      return result;
    }
  }
}
