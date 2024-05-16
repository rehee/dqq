using DQQ.Commons;
using DQQ.Components.Stages;
using DQQ.Components.Stages.Maps;
using DQQ.Consts;
using DQQ.Entities;
using DQQ.Enums;
using DQQ.Pools;
using DQQ.Profiles.Mobs;
using DQQ.Profiles.Skills;
using DQQ.Strategies.SkillStrategies;
using DQQ.Tags;
using ReheeCmf.Helpers;
using ReheeCmf.Responses;

namespace DQQ.Components.Skills
{
  public class SkillComponent : DQQComponent, ISkillComponent
  {
    public static SkillComponent New(EnumSkill skill, int skillSlot = 0)
    {
      var skillComponent = new SkillComponent();
      var skillProfile = DQQPool.SkillPool[skill];
      skillComponent.InitSkillProfile(skillProfile, skillSlot);
      return skillComponent;
    }
    public SkillStrategy[]? SkillStrategies { get; set; }
    public static SkillComponent New(MobSkill skill, int skillSlot = 0)
    {
      var skillComponent = new SkillComponent();
      var skillProfile = DQQPool.SkillPool[skill.SkillNumber];
      skillComponent.InitSkillProfile(skillProfile, skillSlot);
      skillComponent.SkillStrategies = skill.Strategies;
      return skillComponent;
    }
    public int Slot { get; set; }
    public decimal CastTime { get; set; }
    public decimal Cooldown { get; set; }
    public bool CastWithWeaponSpeed { get; set; }
    public decimal DamageRate { get; set; }

    public int CastTick => Convert.ToInt32(CastTime * DQQGeneral.TickPerSecond);
    public int CDTick => Convert.ToInt32(Cooldown * DQQGeneral.TickPerSecond);
    public int CastTickCount { get; set; }
    public int CDTickCount { get; set; }
    public IEnumerable<ITag>? Tags { get; set; }

    public ISkill? SkillProfile { get; protected set; }

    public override void Initialize(IDQQEntity entity)
    {
      base.Initialize(entity);
      if (entity is SkillEntity sp)
      {
        var skillProfile = DQQPool.SkillPool[sp.SkillNumber ?? 0];
        InitSkillProfile(skillProfile, sp.Slot);
      }
    }

    public void InitSkillProfile(ISkill profile, int skillSlot = 0)
    {
      SkillProfile = profile;
      CastTime = profile.CastTime;
      Cooldown = profile.CoolDown;
      DamageRate = profile.DamageRate;
      CastWithWeaponSpeed = profile.CastWithWeaponSpeed;
      this.Slot = skillSlot;
    }

    public int CastWithWeaponSpeedTick(ITarget? caster)
    {
      if (CastWithWeaponSpeed != true || caster?.AttackPerSecond == null || caster?.AttackPerSecond == 0)
      {
        return CastTick;
      }

      return (int)((1 / caster?.AttackPerSecond!) * DQQGeneral.TickPerSecond);

    }

    public virtual async Task<ContentResponse<bool>> OnTick(ITarget? caster, IEnumerable<ITarget>? targets, IMap? map)
    {
      var result = new ContentResponse<bool>();
      var target = caster?.Target;
      if (CDTickCount > 0)
      {
        CDTickCount--;
        return result;
      }
      if (CastTickCount < CastWithWeaponSpeedTick(caster))
      {
        CastTickCount++;
        return result;
      }
      var matchCondition = false;
      if (SkillStrategies != null)
      {
        foreach (var strategy in SkillStrategies.OrderBy(b => b.Priority))
        {
          if (!strategy.Condition)
          {
            matchCondition = true;
            break;
          }
          if (target == null)
          {
            continue;
          }
          switch (strategy.Property)
          {
            case EnumPropertyCompare.HealthPercentage:
              switch (strategy.Compare)
              {
                case EnumCompare.MoreThan:
                  matchCondition = ((decimal)target.CurrentHP / (target.MaximunLife ?? 1)) > strategy.Value;
                  break;
                case EnumCompare.LessThan:
                  matchCondition = ((decimal)target.CurrentHP / (target.MaximunLife ?? 1)) < strategy.Value;
                  break;
              }
              break;
            case EnumPropertyCompare.HealthAmount:
              switch (strategy.Compare)
              {
                case EnumCompare.MoreThan:
                  matchCondition = target.CurrentHP > strategy.Value;
                  break;
                case EnumCompare.LessThan:
                  matchCondition = target.CurrentHP < strategy.Value;
                  break;
              }
              break;
          }
          if (matchCondition)
          {
            break;
          }
        }
      }
      else
      {
        matchCondition = true;
      }

      if (!matchCondition)
      {
        return result;
      }

      if (SkillProfile != null)
      {
        result = await SkillProfile.CastSkill(caster, targets, map);
      }
      else
      {
        result.SetSuccess(true);
      }
      CastTickCount = 0;
      if (CastWithWeaponSpeedTick(caster) <= 0 && CDTick <= 0)
      {
        CDTickCount = (int)(DQQGeneral.MinCooldown * DQQGeneral.TickPerSecond);
      }
      else
      {
        CDTickCount = CDTick;
      }

      return result;
    }

    public virtual SkillEntity ToSkillEntity(Guid? actorId = null)
    {
      var result = new SkillEntity();
      result.Id = DisplayId ?? Guid.NewGuid();
      result.Name = DisplayName;
      result.SkillNumber = SkillProfile?.SkillNumber;
      result.ActorId = actorId;
      return result;
    }
  }
}
