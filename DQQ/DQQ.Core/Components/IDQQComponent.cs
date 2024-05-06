using DQQ.Entities;

namespace DQQ.Components
{
  public interface IDQQComponent
  {
    Guid? DisplayId { get; }
    string? DisplayName { get; }
    IDQQEntity? Profile { get; }
    Task Initialize(IDQQEntity entity);

  }
}
