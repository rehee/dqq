﻿using DQQ.Combats;
using DQQ.Commons;
using DQQ.Components.Durations;
using DQQ.Components.Parameters;
using DQQ.Components.Stages.Maps;
using DQQ.Enums;
using DQQ.Profiles;
using DQQ.Profiles.Durations;
using DQQ.TickLogs;
using ReheeCmf.Responses;
using System.Numerics;

namespace DQQ.Components.Stages;

public interface ITarget : IDQQComponent, ICombatCalculate, IWIthCombatPanel
{
	int Level { get; }
	EnumTargetPriority? TargetPriority { get; }
	EnumTargetLevel PowerLevel { get; }
	ITarget? Target { get; }
	Int64 CurrentHP { get; set; }

	ContentResponse<bool> TryBlock();

	decimal PercentageHP { get; }
	bool Targetable { get; }
	bool Alive { get; }
	void SelectTarget(ITarget? target);
	DamageTaken TakeDamage(ComponentTickParameter parameter);
	void TakeHealing(ComponentTickParameter? parameter);
	HashSet<DurationComponent>? Durations { get; }

	void DamageHandCheck(EnumDamageHand damageHand);
}

