using DQQ.Commons;
using DQQ.Components.Skills;
using DQQ.Components.Stages.Maps;
using ReheeCmf.Responses;
using System.Numerics;

namespace DQQ.Components.Stages.Actors
{
  public interface IActor : ITarget
  {
    Int64 BasicDamage { get; }
    IEnumerable<ISkillComponent> Skills { get; }
  }
}
