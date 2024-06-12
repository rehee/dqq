using DQQ.Entities;
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

    void BeforeDamageReduction(ComponentTickParameter parameter);
    void DamageReduction(ComponentTickParameter parameter);
    void BeforeTakeDamage(ComponentTickParameter parameter);
    void AfterTakeDamage(ComponentTickParameter parameter);
		public Task<ContentResponse<bool>> AfterDealingDamage(ComponentTickParameter parameter);
	}
}
