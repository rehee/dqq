using DQQ.Commons;
using DQQ.Components.Stages;
using DQQ.Components.Stages.Maps;
using DQQ.Enums;
using DQQ.Helper;
using ReheeCmf.Responses;
using ReheeCmf.Helpers;
using DQQ.Components.Parameters;

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
    public virtual async Task<ContentResponse<bool>> CastSkill(ComponentTickParameter? parameter)
    {
      await Task.CompletedTask;
      var response = new ContentResponse<bool>();
      var selectedTarget = parameter?.SelectedTarget;
      if (selectedTarget?.Alive != true)
      {
        return response;
      }
      parameter?.Map!.AddMapLogSpillCast(true, parameter?.From, parameter?.SelectedTarget, this);
      response.SetSuccess(true);
      if (SkillType == EnumSkillType.Damage || SkillType == EnumSkillType.hybrid)
      {
        var damage = CalculateDamage(parameter?.From, parameter?.Map);
        DealingDamage(parameter?.From, parameter?.SelectedTarget, damage, parameter?.Map);
      }

      if (SkillType == EnumSkillType.Healing || SkillType == EnumSkillType.hybrid)
      {
        var healing = CalculateHealing(parameter?.From, parameter?.Map);
        DealingHealing(parameter?.From, healing, parameter?.Map);
      }


      return response;
    }

    public virtual SkillHitCheck? CheckHit(ITarget? caster, ITarget? skillTarget, IMap? map)
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
