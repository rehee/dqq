using DQQ.Commons;
using DQQ.Components.Stages.Maps;
using DQQ.Entities;
using System.Numerics;

namespace DQQ.Components.Stages
{
  public abstract class TargetBase : DQQComponent, ITarget
  {
    public bool Targetable { get; set; }
    public bool Alive { get; set; }
    public ITarget? Target { get; set; }
    public virtual Int64? MaximunLife { get; set; }
    public virtual Int64? Armor { get; set; }
    public virtual Int64? Damage { get; set; }
    public virtual decimal? AttackPerSecond { get; set; }
    public virtual decimal? ArmorPercentage { get; set; }
    public virtual decimal? Resistance { get; set; }
    public Int64? MainHand { get; set; }
    public Int64? OffHand { get; set; }
    public bool? PrevioursMainHand { get; set; }
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
  }
}
