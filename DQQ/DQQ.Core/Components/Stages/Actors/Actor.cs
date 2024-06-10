using DQQ.Commons;
using DQQ.Components.Items;
using DQQ.Components.Parameters;
using DQQ.Components.Skills;
using DQQ.Components.Stages.Maps;
using DQQ.Entities;
using DQQ.Enums;
using DQQ.Helper;
using DQQ.Profiles;
using DQQ.TickLogs;
using ReheeCmf.Helpers;
using ReheeCmf.Responses;
using System.Numerics;
using System.Text.Json.Serialization;

namespace DQQ.Components.Stages.Actors
{
  public class Actor : TargetBase, IActor
  {
    public Int64 BasicDamage { get; set; }

    public override EnumTargetLevel PowerLevel => EnumTargetLevel.NotSpecified;

    [JsonIgnore]
    public IEnumerable<SkillComponent>? Skills { get; set; }

    public override void Initialize(IDQQEntity entity)
    {
      base.Initialize(entity);
      if (entity is ActorEntity ae)
      {
        TargetPriority = ae.TargetPriority;
        CombatPanel.StaticPanel.MaximunLife = ae.MaxHP ?? 0;
        CurrentHP = CombatPanel.StaticPanel.MaximunLife ?? 0;
        BasicDamage = ae.BasicDamage ?? 0;
        var list = new List<SkillComponent>();
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

    public override async Task<ContentResponse<bool>> OnTick(ComponentTickParameter parameter)
    {
      var result = await base.OnTick(parameter);
      if (!result.Success)
      {
        return result;
      }
      if (Skills != null)
      {
        foreach (var skill in Skills.Where(b => b != null))
        {
          var skillResult = await skill!.OnTick(parameter);
        }
      }
      return result;
    }


  }
}
