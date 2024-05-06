using DQQ.Components;
using ReheeCmf.Entities;

namespace DQQ.Entities
{
  public abstract class DQQEntityBase<T> : EntityBase<Guid>, IDQQEntity<T> where T : IDQQComponent, new()
  {
    public string? Name { get; set; }

    public virtual async Task<T> GenerateComponent()
    {
      var instance = new T();
      await instance.Initialize(this);
      return instance;
    }
  }
}
