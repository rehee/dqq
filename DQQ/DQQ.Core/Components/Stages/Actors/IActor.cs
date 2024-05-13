﻿using DQQ.Commons;
using DQQ.Components.Skills;
using DQQ.Components.Stages.Maps;
using ReheeCmf.Responses;
using System.Numerics;

namespace DQQ.Components.Stages.Actors
{
  public interface IActor : ITarget
  {
    int Level { get; }

    Int64 CurrentHP { get; }
    Int64 BasicDamage { get; }
    Dictionary<int, ISkillComponent?>? Skills { get; }


    Task<ContentResponse<bool>> OnTick(IEnumerable<ITarget>? targets, IMap map);
  }
}
