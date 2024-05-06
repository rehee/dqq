using DQQ.Commons;
using DQQ.Components.Items;
using DQQ.Components.Skills;
using DQQ.Components.Stages.Maps;
using DQQ.Entities;
using DQQ.TickLogs;
using ReheeCmf.Helpers;
using ReheeCmf.Responses;
using System.Numerics;

namespace DQQ.Components.Stages.Actors
{
  public class Actor : TargetBase, IActor
  {
    public int Level { get; }

    public BigInteger CurrentHP { get; set; }

    public BigInteger BasicDamage { get; set; }


    public Dictionary<int, ISkillComponent?>? Skills { get; set; }

    public override async Task Initialize(IDQQEntity entity)
    {
      await base.Initialize(entity);
      if (entity is ActorEntity ae)
      {
        MaximunLife = BigInteger.Parse(ae.MaxHP ?? "0");
        CurrentHP = MaximunLife ?? 0;
        BasicDamage = BigInteger.Parse(ae.BasicDamage ?? "0");
        Skills = new Dictionary<int, ISkillComponent?>();
        if (ae.Skills?.Any() == true)
        {
          var skills = ae.Skills!.DistinctBy(b => b.Slot).ToArray();

          foreach (var skill in skills)
          {
            var s = await skill.GenerateComponent();
            Skills.Add(skill.Slot, s);
          }
        }
      }
    }

    public virtual async Task<ContentResponse<bool>> OnTick(IEnumerable<ITarget>? targets, IMap? map)
    {
      var result = new ContentResponse<bool>();
      if (!Alive)
      {
        return result;
      }
      result.SetSuccess(true);
      if (Skills != null)
      {
        foreach (var skill in Skills.Where(b => b.Value != null))
        {
          var skillResult = await skill.Value!.OnTick(this, targets, map);
        }
      }
      return result;
    }
    protected object lockObject = new object();
    public override DamageTaken TakeDamage(ITarget? from, BigInteger damage, IMap? map)
    {
      lock (lockObject)
      {
        var result = new DamageTaken();
        if (!Alive)
        {
          return result;
        }
        BigInteger damageTaken = damage;
        bool isDead = false;
        CurrentHP = CurrentHP - damageTaken;
        if (CurrentHP <= 0)
        {
          Alive = false;
          isDead = true;
        }
        result.DamagePoint = damageTaken;
        result.IsKilled = isDead;
        result.DamageTakenSuccess = true;
        return result;
      }

    }
  }
}
