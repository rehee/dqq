using DQQ.Entities;

namespace DQQ.Components
{
  public abstract class DQQComponent : IDQQComponent
  {
    public virtual Guid? DisplayId { get; set; }
    public virtual string? DisplayName { get; set; }

    public IDQQEntity? Profile { get; set; }

    public virtual async Task Initialize(IDQQEntity entity)
    {
      await Task.CompletedTask;
      Profile = Profile;
      DisplayId = entity.Id;
      DisplayName = entity.Name;
    }
  }
}
