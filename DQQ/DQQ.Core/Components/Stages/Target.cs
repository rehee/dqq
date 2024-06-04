using DQQ.Combats;
using DQQ.Commons;
using DQQ.Components.Durations;
using DQQ.Components.Parameters;
using DQQ.Components.Stages.Maps;
using DQQ.Consts;
using DQQ.Entities;
using DQQ.Enums;
using DQQ.Helper;
using DQQ.Profiles;
using DQQ.Profiles.Durations;
using DQQ.Profiles.Skills;
using DQQ.TickLogs;
using ReheeCmf.Helpers;
using ReheeCmf.Responses;
using System.Numerics;

namespace DQQ.Components.Stages
{
  public abstract class TargetBase : DQQComponent, ITarget
  {
    public int Level { get; set; }
    public bool Targetable { get; set; }
    public bool Alive { get; set; }
    public ITarget? Target { get; set; }
    public EnumTargetPriority? TargetPriority { get; set; }
    public Int64 CurrentHP { get; set; }


    public bool? PrevioursMainHand { get; set; }

    public abstract EnumTargetLevel PowerLevel { get; }

    public virtual decimal PercentageHP => (CombatPanel.DynamicPanel.MaximunLife == null || CombatPanel.DynamicPanel.MaximunLife == 0) ? 1 : (CurrentHP / (decimal)CombatPanel.DynamicPanel.MaximunLife);

    public HashSet<DurationComponent>? Durations { get; set; } = new HashSet<DurationComponent>();

    public CombatPanel CombatPanel { get; set; } = new CombatPanel();

    public void SelectTarget(ITarget? target)
    {
      Target = target;
    }

    

    protected virtual void TakingDamage(DamageTakenParameter parameter)
    {
      if (parameter?.Damage == null || parameter?.Damage?.DamageTakenSuccess != true)
      {
        return;
      }
      if (!Alive)
      {
        parameter.Damage.DamageTakenSuccess = false;
        return;
      }
      Int64 damageTaken = parameter.Damage.DamagePoint;
      bool isDead = false;
      CurrentHP = CurrentHP - damageTaken;
      if (CurrentHP <= 0)
      {
        Alive = false;
        isDead = true;
      }
      parameter.Damage.DamagePoint = damageTaken;
      parameter.Damage.IsKilled = isDead;
      parameter.Damage.DamageTakenSuccess = true;
    }
    
    protected object lockObject = new object();

    public override void Initialize(IDQQEntity profile)
    {
      base.Initialize(profile);
      Targetable = true;
      Alive = true;
    }
    protected int blockingCount { get; set; }
    public override async Task<ContentResponse<bool>> OnTick(ComponentTickParameter parameter)
    {
      var result = new ContentResponse<bool>();
      if (Alive)
      {
        result.SetSuccess(true);
      }
      if (result.Success)
      {
        if (blockingCount > 0)
        {
          blockingCount--;
        }
        var durations = Durations!.ToArray();
        foreach (var d in durations)
        {
          await d.OnTick(parameter);
        }
        var durationNeedRemove = durations.Where(b => b.TickRemain < 0);
        foreach (var d in durationNeedRemove)
        {
          Durations?.Remove(d);
          await d.DisposeAsync();
        }
      }
      else
      {
        Durations = null;
      }

      return result;
    }

    protected override void SelfBeforeDamageReduction(BeforeDamageTakenParameter parameter)
    {
      if (this.Durations?.Any() != true)
      {
        return;
      }
      foreach (var d in Durations.ToArray())
      {
        d.BeforeDamageReduction(parameter);
      }
      base.SelfBeforeDamageReduction(parameter);
    }
    protected override void SelfDamageReduction(BeforeDamageTakenParameter parameter)
    {
      if (this.Durations?.Any() != true)
      {
        return;
      }
      foreach (var d in Durations.ToArray())
      {
        d.DamageReduction(parameter);
      }
      base.SelfDamageReduction(parameter);
    }

    protected override void SelfBeforeTakeDamage(DamageTakenParameter parameter)
    {
      if (this.Durations?.Any() != true)
      {
        return;
      }
      foreach (var d in Durations.ToArray())
      {
        d.BeforeTakeDamage(parameter);
      }
      base.SelfBeforeTakeDamage(parameter);
    }
    protected override void SelfAfterTakeDamage(DamageTakenParameter parameter)
    {
      if (this.Durations?.Any() != true)
      {
        return;
      }
      foreach (var d in Durations.ToArray())
      {
        d.AfterTakeDamage(parameter);
      }
      base.SelfAfterTakeDamage(parameter);
    }

    protected virtual EnumHitCheck HitCheck(BeforeDamageTakenParameter parameter)
    {
      if (parameter.Source is DurationProfile)
      {
        return EnumHitCheck.Hit;
      }
      SkillHitCheck? skillHitOverride = null;
      if (parameter.Source is SkillProfile sp)
      {
        skillHitOverride = sp.CheckHit(parameter);
        if (skillHitOverride?.HitCheck == EnumHitCheck.Hit)
        {
          return EnumHitCheck.Hit;
        }
      }

      return HitCheckHelper.HitCheck(parameter, skillHitOverride);
    }
    public virtual DamageTaken TakeDamage(BeforeDamageTakenParameter parameter)
    {
      //damage reduction
      var hitResult = HitCheck(parameter);
      if (hitResult == EnumHitCheck.Hit)
      {
        if (parameter.Damages?.Any() != true || parameter.Damages?.All(b => b.DamagePoint == 0) == true)
        {
          return DamageTaken.New(hitResult, false);
        }

        BeforeDamageReduction(parameter);
        DamageReduction(parameter);

        var result = DamageTaken.New(parameter.Damages?.ToArray() ?? [], false);
        result.DamageTakenSuccess = this.Alive;
        var damageTaken = DamageTakenParameter.New(parameter, result);
        BeforeTakeDamage(damageTaken);
        TakingDamage(damageTaken);
        AfterTakeDamage(damageTaken);

        parameter.Map.AddMapLogDamageTaken(damageTaken);
        return result;
      }
      else
      {
        var result = DamageTaken.New(hitResult, false);
        result.DamageTakenSuccess = false;
        return result;
      }

    }
    public virtual void TakeHealing(ITarget? from, long healing, IMap? map, IDQQProfile? source)
    {
      if (!Alive)
      {
        return;
      }
      var hpOverHealing = (this?.CurrentHP ?? 0) + healing - (this?.CombatPanel.DynamicPanel.MaximunLife ?? 0);
      if (hpOverHealing > 0)
      {
        this.CurrentHP = this?.CombatPanel.DynamicPanel.MaximunLife ?? 0;
      }
      else
      {
        this.CurrentHP = this.CurrentHP + healing;
      }
      map.AddMapLogHealingTaken(true, from, this, source, new TickLogHealing(healing, hpOverHealing > 0 ? hpOverHealing : null));
    }

    public virtual ContentResponse<bool> TryBlock()
    {
      var result = new ContentResponse<bool>();
      if (blockingCount > 0)
      {
        return result;
      }
      result.SetSuccess(true);
      var blockTick = DQQGeneral.BlockRecoveryTime * DQQGeneral.TickPerSecond;
      var blockRecovery = 1 + CombatPanel.DynamicPanel.BlockRecovery.DefaultValue();
      blockingCount = (int)Math.Round(blockTick / blockRecovery, 0);
      return result;
    }
  }
}
