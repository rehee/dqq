using DQQ.Components.Stages.Maps;
using DQQ.Components.Stages;
using DQQ.Entities;
using DQQ.Enums;
using DQQ.Profiles;
using ReheeCmf.Responses;
using DQQ.Components.Parameters;

namespace DQQ.Components
{
  public interface IDQQComponent : IAsyncDisposable
  {
    Guid? DisplayId { get; }
    string? DisplayName { get; }

    IDQQEntity? Entity { get; }

		DQQProfile? Profile { get; }
    void Initialize(IDQQEntity entity, DQQComponent? parent);
    Task<ContentResponse<bool>> OnTick(ComponentTickParameter parameter);

    void BeforeDamageReduction(BeforeDamageTakenParameter parameter);
    void DamageReduction(BeforeDamageTakenParameter parameter);
    void BeforeTakeDamage(DamageTakenParameter parameter);
    void AfterTakeDamage(DamageTakenParameter parameter);
  }
}
