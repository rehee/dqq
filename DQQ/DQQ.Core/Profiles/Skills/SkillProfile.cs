using DQQ.Commons;
using DQQ.Components;
using DQQ.Components.Stages;
using DQQ.Components.Stages.Maps;
using DQQ.Consts;
using DQQ.Enums;
using DQQ.Helper;
using DQQ.TickLogs;
using DQQ.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Numerics;
using ReheeCmf.Responses;
using ReheeCmf.Helpers;

namespace DQQ.Profiles.Skills
{
  public abstract class SkillProfile : DQQProfile<EnumSkill>, ISkill
  {
    public abstract bool NoPlayerSkill { get; }
    public abstract decimal CastTime { get; }
    public abstract decimal CoolDown { get; }
    public abstract decimal DamageRate { get; }
    public abstract bool CastWithWeaponSpeed { get; }
    public EnumSkill SkillNumber => ProfileNumber;

    public string? SkillName => Name;



    public virtual Int64 CalculateDamage(ITarget? caster, IMap? map)
    {
      return DamageHelper.SkillDamage(this, caster!, map);
    }

    public virtual async Task<ContentResponse<bool>> CastSkill(ITarget? caster, ITarget? skillTarget, IEnumerable<ITarget>? target, IMap? map)
    {
      await Task.CompletedTask;
      var response = new ContentResponse<bool>();
      if (caster?.Target != null && caster?.Target.Alive == true)
      {
        response.SetSuccess(true);
        var damage = CalculateDamage(caster, map);
        DamageTaken? result;
        if (skillTarget != null && damage > 0)
        {
          result = skillTarget.TakeDamage(caster, damage, map);
        }
        else
        {
          result = caster.Target.TakeDamage(caster, damage, map);
        }
        if (result != null)
        {
          map!.AddMapLog(true, caster, skillTarget ?? caster.Target, this, result);
        }

      }
      return response;
    }
  }
}
