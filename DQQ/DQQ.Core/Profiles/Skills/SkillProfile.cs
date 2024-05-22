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
    public virtual EnumDamageHand DamageHand => EnumDamageHand.Any;
    public abstract bool NoPlayerSkill { get; }
    protected virtual bool SelfTarget { get; }
    public abstract decimal CastTime { get; }
    public abstract decimal CoolDown { get; }
    public abstract decimal DamageRate { get; }
    public abstract bool CastWithWeaponSpeed { get; }
    public EnumSkill SkillNumber => ProfileNumber;

    public string? SkillName => Name;



    public virtual Int64 CalculateDamage(ITarget? caster, IMap? map)
    {
      return DamageHelper.SkillDamage(this, caster!, map).SkillMordifier(caster);
    }

    protected virtual void DealingDamage(ITarget? caster, ITarget? skillTarget, long damage, IMap? map)
    {
      if (skillTarget != null && damage > 0)
      {
        skillTarget.TakeDamage(caster, damage, map, this);
      }
      else
      {
        caster.Target.TakeDamage(caster, damage, map, this);
      }
      if (DamageHand == EnumDamageHand.EachHand && caster.CombatPanel.IsDuelWield)
      {
        if (caster.PrevioursMainHand == null)
        {
          caster.PrevioursMainHand = true;
        }
        else
        {
          caster.PrevioursMainHand = !caster.PrevioursMainHand;
        }
      }
    }
    public virtual async Task<ContentResponse<bool>> CastSkill(ITarget? caster, ITarget? skillTarget, IEnumerable<ITarget>? target, IMap? map)
    {
      await Task.CompletedTask;
      var response = new ContentResponse<bool>();
      var selectedTarget = SelfTarget ? caster : (skillTarget ?? caster?.Target);
      if (selectedTarget?.Alive == true)
      {
        response.SetSuccess(true);
        map!.AddMapLogSpillCast(true, caster, skillTarget ?? caster.Target, this);
        var damage = CalculateDamage(caster, map);
        DealingDamage(caster, skillTarget, damage, map);
      }
      return response;
    }
  }
}
