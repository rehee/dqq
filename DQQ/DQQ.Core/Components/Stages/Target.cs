using DQQ.Combats;
using DQQ.Commons;
using DQQ.Components.Durations;
using DQQ.Components.Stages.Maps;
using DQQ.Entities;
using DQQ.Enums;
using DQQ.Helper;
using DQQ.Profiles;
using DQQ.TickLogs;
using ReheeCmf.Helpers;
using ReheeCmf.Responses;
using System.Numerics;

namespace DQQ.Components.Stages
{
  public abstract class TargetBase : DQQComponent, ITarget
  {
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

    public override async Task<ContentResponse<bool>> OnTick(ITarget? owner, IEnumerable<ITarget>? targets, IMap? map)
    {
      var result = new ContentResponse<bool>();
      if (Alive)
      {
        result.SetSuccess(true);
      }
      if (result.Success)
      {
        var durations = Durations!.ToArray();
        foreach (var d in durations)
        {
          await d.OnTick(this, map);
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
    public virtual DamageTaken TakeDamage(ITarget? from, DamageDeal[] damage, IMap? map, IDQQProfile? source)
    {
      //damage reduction
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
  }
}
