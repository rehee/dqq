using DQQ.Commons;
using DQQ.Components.Items;
using DQQ.Components.Skills;
using DQQ.Components.Stages.Maps;
using DQQ.Entities;
using DQQ.TickLogs;
using ReheeCmf.Helpers;
using ReheeCmf.Responses;
using System.Numerics;
using System.Text.Json.Serialization;

namespace DQQ.Components.Stages.Actors
{
  public class Actor : TargetBase, IActor
  {
    public int Level { get; }

    

    public Int64 BasicDamage { get; set; }

    public override int PowerLevel => 0;

    [JsonIgnore]
    public IEnumerable<ISkillComponent>? Skills { get; set; }

    public override void Initialize(IDQQEntity entity)
    {
      base.Initialize(entity);
      if (entity is ActorEntity ae)
      {
        TargetPriority = ae.TargetPriority;
        MaximunLife = ae.MaxHP ?? 0;
        CurrentHP = MaximunLife ?? 0;
        BasicDamage = ae.BasicDamage ?? 0;
        var list = new List<ISkillComponent>();
        Skills = list;
        if (ae.Skills?.Any() == true)
        {
          var skills = ae.Skills!.DistinctBy(b => b.Slot).ToArray();

          foreach (var skill in skills)
          {
            var s = skill.GenerateComponent();
            list.Add(s);
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
        foreach (var skill in Skills.Where(b => b != null))
        {
          var skillResult = await skill!.OnTick(this, targets, map);
        }
      }
      return result;
    }
    protected object lockObject = new object();
    public override DamageTaken TakeDamage(ITarget? from, Int64 damage, IMap? map)
    {
      lock (lockObject)
      {
        var result = new DamageTaken();
        if (!Alive)
        {
          return result;
        }
        Int64 damageTaken = damage;
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
