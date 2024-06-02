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

    protected virtual void BeforeTakeDamage(ITarget? from, DamageTaken damage, IMap? map, IDQQProfile? source)
    {

    }
    protected virtual void DamageReduction(ITarget? from, DamageTaken damage, IMap? map, IDQQProfile? source)
    {

    }
    protected virtual void TakingDamage(ITarget? from, DamageTaken damage, IMap? map, IDQQProfile? source)
    {
      if (!damage.DamageTakenSuccess)
      {
        return;
      }
      if (!Alive)
      {
        damage.DamageTakenSuccess = false;
        return;
      }
      Int64 damageTaken = damage.DamagePoint;
      bool isDead = false;
      CurrentHP = CurrentHP - damageTaken;
      if (CurrentHP <= 0)
      {
        Alive = false;
        isDead = true;
      }
      damage.DamagePoint = damageTaken;
      damage.IsKilled = isDead;
      damage.DamageTakenSuccess = true;
    }
    protected virtual void AfterTakeDamage(ITarget? from, DamageTaken damage, IMap? map, IDQQProfile? source)
    {

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
          d.Dispose();
        }
      }
      else
      {
        Durations = null;
      }

      return result;
    }
    protected virtual DamageDeal[] DamageReduction(ITarget? from, DamageDeal[] damage, IMap? map, IDQQProfile? source)
    {
      return damage.Select(b => b).ToArray();
    }
    protected virtual EnumHitCheck HitCheck(ITarget? from, ITarget? to, IMap? map, IDQQProfile? source)
    {
      if (source is DurationProfile)
      {
        return EnumHitCheck.Hit;
      }
      SkillHitCheck? skillHitOverride = null;
      if (source is SkillProfile sp)
      {
        skillHitOverride = sp.CheckHit(from, this, map);
        if (skillHitOverride?.HitCheck == EnumHitCheck.Hit)
        {
          return EnumHitCheck.Hit;
        }
      }

      return HitCheckHelper.HitCheck(from, to, map, skillHitOverride);
    }
    public virtual DamageTaken TakeDamage(ITarget? from, DamageDeal[] damage, IMap? map, IDQQProfile? source)
    {
      //damage reduction
      var hitResult = HitCheck(from, this, map, source);
      if (hitResult == EnumHitCheck.Hit)
      {
        if (damage.Length <= 0 || damage.All(b => b.DamagePoint == 0))
        {
          return DamageTaken.New(hitResult, false);
        }
        var damageAfterReduction = DamageReduction(from, damage, map, source);
        var result = DamageTaken.New(damage, false);
        result.DamageTakenSuccess = this.Alive;
        BeforeTakeDamage(from, result, map, source);
        DamageReduction(from, result, map, source);
        TakingDamage(from, result, map, source);
        AfterTakeDamage(from, result, map, source);
        map.AddMapLogDamageTaken(result.DamageTakenSuccess, from, this, source, result);
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
