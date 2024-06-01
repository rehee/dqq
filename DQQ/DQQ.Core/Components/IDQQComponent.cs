﻿using DQQ.Components.Stages.Maps;
using DQQ.Components.Stages;
using DQQ.Entities;
using DQQ.Enums;
using DQQ.Profiles;
using ReheeCmf.Responses;

namespace DQQ.Components
{
  public interface IDQQComponent : IDisposable
  {
    Guid? DisplayId { get; }
    string? DisplayName { get; }

    IDQQEntity? Entity { get; }

    IDQQProfile? Profile { get; }
    void Initialize(IDQQEntity entity);
    Task<ContentResponse<bool>> OnTick(ITarget? owner, IEnumerable<ITarget>? targets, IMap? map);
  }
}
