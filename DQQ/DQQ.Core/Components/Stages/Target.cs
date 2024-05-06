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
    public virtual BigInteger? MaximunLife { get; set; }
    public virtual BigInteger? Armor { get; set; }
    public virtual BigInteger? Damage { get; set; }
    public virtual decimal? AttackPerSecond { get; set; }
    public virtual decimal? ArmorPercentage { get; set; }
    public virtual decimal? Resistance { get; set; }
    public BigInteger? MainHand { get; set; }
    public BigInteger? OffHand { get; set; }
    public bool? PrevioursMainHand { get; set; }
    public void SelectTarget(ITarget? target)
    {
      Target = target;
    }

    public virtual DamageTaken TakeDamage(ITarget? from, BigInteger damage, IMap? map)
    {
      return DamageTaken.New(damage, false);
    }
    public override async Task Initialize(IDQQEntity profile)
    {
      await base.Initialize(profile);
      Targetable = true;
      Alive = true;
    }
  }
}
