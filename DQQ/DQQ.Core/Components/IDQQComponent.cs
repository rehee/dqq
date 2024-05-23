using DQQ.Entities;
using DQQ.Enums;
using DQQ.Profiles;

namespace DQQ.Components
{
  public interface IDQQComponent
  {
    Guid? DisplayId { get; }
    string? DisplayName { get; }
    
    IDQQEntity? Entity { get; }

    IDQQProfile? Profile { get; }
    void Initialize(IDQQEntity entity);

  }
}
