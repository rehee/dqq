using DQQ.Entities;
using DQQ.Enums;

namespace DQQ.Components
{
  public interface IDQQComponent
  {
    Guid? DisplayId { get; }
    string? DisplayName { get; }
    
    IDQQEntity? Profile { get; }
    void Initialize(IDQQEntity entity);

  }
}
