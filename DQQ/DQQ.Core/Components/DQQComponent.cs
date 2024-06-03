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

    bool IsDisposed { get; set; }
    public virtual async ValueTask DisposeAsync()
    {
      await Task.CompletedTask;
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
      await Task.CompletedTask;
      var result = new ContentResponse<bool>();
      result.SetSuccess(parameter.From?.Alive == true);
      if (result.Success)
      {
        if (AfterDealingDamageCount > 0)
        {
          AfterDealingDamageCount--;
        }
      }
      return result;
    }



    protected int AfterDealingDamageCount { get; set; }
  }
}
