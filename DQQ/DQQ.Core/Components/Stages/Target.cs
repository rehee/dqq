using DQQ.Commons;
using DQQ.Components.Durations;
using DQQ.Components.Stages.Maps;
using DQQ.Entities;
using DQQ.Enums;
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

    public virtual DamageTaken TakeDamage(ITarget? from, Int64 damage, IMap? map)
    {
      return DamageTaken.New(damage, false);
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
      result.SetSuccess(true);

      var durations = Durations!.ToArray();
      foreach (var d in durations)
      {
        if (Alive)
        {
          await d.OnTick(this, map);
        }
      }
      var durationNeedRemove = durations.Where(b => b.TickRemain < 0);
      foreach (var d in durationNeedRemove)
      {
        Durations?.Remove(d);
        d.Dispose();
      }
      return result;
    }
  }
}
