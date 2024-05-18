using DQQ.Commons;
using DQQ.Components.Stages;
using DQQ.Components.Stages.Maps;
using DQQ.Consts;
using DQQ.Entities;
using DQQ.Enums;
using DQQ.Helper;
using DQQ.Pools;
using DQQ.Profiles.Mobs;
using DQQ.Profiles.Skills;
using DQQ.Strategies.SkillStrategies;
using DQQ.Tags;
using ReheeCmf.Commons.Jsons.Options;
using ReheeCmf.Helpers;
using ReheeCmf.Responses;
using System.Text.Json;

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
        SkillStrategies = String.IsNullOrEmpty(sp.SkillStrategy) ? null :
          JsonSerializer.Deserialize<SkillStrategy[]?>(sp.SkillStrategy ?? "", JsonOption.DefaultOption);
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
      (bool, ITarget?) matchCondition = (false, null);
      if (SkillStrategies != null)
      {
        matchCondition = StrategyHelper.MatchSkillStrategy(SkillStrategies, caster, targets, map);
      }
      else
      {
        matchCondition = (true, null);
      }

      if (!matchCondition.Item1)
      {
        return result;
      }

      if (SkillProfile != null)
      {
        result = await SkillProfile.CastSkill(caster, matchCondition.Item2, targets, map);
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
