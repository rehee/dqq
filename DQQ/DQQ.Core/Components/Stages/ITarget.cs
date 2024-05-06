using DQQ.Combats;
using DQQ.Commons;
using DQQ.Components.Stages.Maps;
using System.Numerics;

namespace DQQ.Components.Stages;

public interface ITarget : IDQQComponent, ICombatProperty
{
  ITarget? Target { get; }
  bool Targetable { get; }
  bool Alive { get; }
  void SelectTarget(ITarget? target);
  DamageTaken TakeDamage(ITarget? from, BigInteger damage, IMap? map);
}

