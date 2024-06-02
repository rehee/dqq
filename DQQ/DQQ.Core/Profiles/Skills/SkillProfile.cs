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
    public virtual EnumSkillType SkillType => EnumSkillType.Damage;
    public string? SkillName => Name;



    public virtual DamageDeal[] CalculateDamage(ITarget? caster, IMap? map)
    {
      return DamageHelper.SkillDamage(this, caster!, map).SkillMordifier(caster);
    }

    protected virtual void DealingDamage(ITarget? caster, ITarget? skillTarget, DamageDeal[] damageDeals, IMap? map)
    {
      var damageWithDeal = damageDeals.Where(b => b.DamagePoint > 0).ToArray();
      //check hit rate
      //check damage reduction
      //check after dealing damage
      DamageTaken? damageTaken;
      if (skillTarget != null)
      {
        damageTaken = skillTarget.TakeDamage(caster, damageWithDeal, map, this);
      }
      else
      {
        damageTaken = caster?.Target?.TakeDamage(caster, damageWithDeal, map, this);
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
      if (damageTaken?.HitCheck == EnumHitCheck.Hit)
      {
        AfterDealingDamage(caster, skillTarget, damageTaken, map);
      }
    }
    protected virtual void AfterDealingDamage(ITarget? caster, ITarget? skillTarget, DamageTaken? damageTaken, IMap? map)
    {

    }
    public virtual async Task<ContentResponse<bool>> CastSkill(ITarget? caster, ITarget? skillTarget, IEnumerable<ITarget>? target, IMap? map)
    {
      await Task.CompletedTask;
      var response = new ContentResponse<bool>();
      var selectedTarget = SelfTarget ? caster : (skillTarget ?? caster?.Target);
      if (selectedTarget?.Alive != true)
      {
        return response;
      }
      map!.AddMapLogSpillCast(true, caster, skillTarget ?? caster.Target, this);
      response.SetSuccess(true);
      //check skill hit
      if (SkillType == EnumSkillType.Damage || SkillType == EnumSkillType.hybrid)
      {
        var damage = CalculateDamage(caster, map);
        DealingDamage(caster, skillTarget, damage, map);
      }

      if (SkillType == EnumSkillType.Healing || SkillType == EnumSkillType.hybrid)
      {
        var healing = CalculateHealing(caster, map);
        DealingHealing(caster, healing, map);
      }


      return response;
    }

    public virtual EnumHitCheck? CheckHit(ITarget? caster, ITarget? skillTarget, IMap? map)
    {
      return null;
    }

    protected virtual HealingDeal[] CalculateHealing(ITarget? caster, IMap? map)
    {
      return [];
    }
    protected virtual void DealingHealing(ITarget? from, HealingDeal[] healings, IMap? map)
    {
      foreach (var h in healings.Where(b => b.HealingType == EnumHealingType.DirectHeal))
      {
        from?.TakeHealing(from, h.Points, map, this);
      }
    }
  }
}
