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
    public virtual Int64? MaximunLife { get; set; }
    public Int64 CurrentHP { get; set; }
    public virtual Int64? Armor { get; set; }
    public virtual Int64? Damage { get; set; }
    public virtual decimal? AttackPerSecond { get; set; }
    public virtual decimal? ArmorPercentage { get; set; }
    public virtual decimal? Resistance { get; set; }
    public Int64? MainHand { get; set; }
    public Int64? OffHand { get; set; }
    public bool? PrevioursMainHand { get; set; }

    public abstract int PowerLevel { get; }

    public virtual decimal PercentageHP => (MaximunLife == null || MaximunLife == 0) ? 1 : (CurrentHP / (decimal)MaximunLife);

    public HashSet<DurationComponent>? Durations { get; set; } = new HashSet<DurationComponent>();

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
    public virtual DamageTaken TakeDamage(ITarget? from, Int64 damage, IMap? map, IDQQProfile? source)
    {
      var result = DamageTaken.New(damage, false);
      result.DamageTakenSuccess = this.Alive;
      BeforeTakeDamage(from, result, map, source);
      DamageReduction(from, result, map, source);
      TakingDamage(from, result, map, source);
      AfterTakeDamage(from, result, map, source);
      map.AddMapLogDamageTaken(result.DamageTakenSuccess, from, this, source, result);
      return result;
    }
    public override void Initialize(IDQQEntity profile)
    {
      base.Initialize(profile);
      Targetable = true;
      Alive = true;
    }

    public virtual async Task<ContentResponse<bool>> OnTick(IEnumerable<ITarget>? targets, IMap? map)
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

    public virtual void TakeHealing(ITarget? from, long healing, IMap? map, IDQQProfile? source)
    {
      if (!Alive)
      {
        return;
      }
      var hpOverHealing = (this?.CurrentHP ?? 0) + healing - (this?.MaximunLife ?? 0);
      if (hpOverHealing > 0)
      {
        this.CurrentHP = this?.MaximunLife ?? 0;
      }
      else
      {
        this.CurrentHP = this.CurrentHP + healing;
      }
      map.AddMapLogHealingTaken(true, from, this, source, new TickLogHealing(healing, hpOverHealing > 0 ? hpOverHealing : null));
    }
  }
}
