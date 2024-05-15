using DQQ.Components;
using DQQ.Components.Stages.Maps;

namespace DQQ.Services.MapServices
{
  public interface IMapService
  {
    Task<IMap?> CreateMap(IDQQComponent? creator, int? mapTier, int? mapSubTier);
  }
}
