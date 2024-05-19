﻿using DQQ.Combats;
using DQQ.Commons;
using DQQ.Components.Durations;
using DQQ.Components.Stages.Maps;
using DQQ.Enums;
using DQQ.Profiles.Durations;
using ReheeCmf.Responses;
using System.Numerics;

namespace DQQ.Components.Stages;

public interface ITarget : IDQQComponent, ICombatProperty, ICombatCalculate
{
  EnumTargetPriority? TargetPriority { get; }
  int PowerLevel { get; }
  ITarget? Target { get; }
  Int64 CurrentHP { get; set; }

  decimal PercentageHP { get; }
  bool Targetable { get; }
  bool Alive { get; }
  void SelectTarget(ITarget? target);
  DamageTaken TakeDamage(ITarget? from, Int64 damage, IMap? map);

  Task<ContentResponse<bool>> OnTick(IEnumerable<ITarget>? targets, IMap? map);
  HashSet<DurationComponent>? Durations { get; }
}

