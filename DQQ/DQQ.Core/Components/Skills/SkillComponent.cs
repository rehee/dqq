using DQQ.Commons;
using DQQ.Components.Stages;
using DQQ.Components.Stages.Maps;
using DQQ.Consts;
using DQQ.Entities;
using DQQ.Enums;
using DQQ.Pools;
using DQQ.Profiles.Skills;
using DQQ.Tags;
using ReheeCmf.Helpers;
using ReheeCmf.Responses;

namespace DQQ.Components.Skills
{
  public class SkillComponent : DQQComponent, ISkillComponent
  {
    public static SkillComponent New(EnumSkill skill)
    {
      var skillComponent = new SkillComponent();
      var skillProfile = DQQPool.SkillPool[skill];
      skillComponent.InitSkillProfile(skillProfile);
      return skillComponent;
    }
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

    public override async Task Initialize(IDQQEntity entity)
    {
      await base.Initialize(entity);
      if (entity is SkillEntity sp)
      {
        var skillProfile = DQQPool.SkillPool[sp.SkillNumber];
        InitSkillProfile(skillProfile);
      }
    }

    public void InitSkillProfile(ISkill profile)
    {
      SkillProfile = profile;
      CastTime = profile.CastTime;
      Cooldown = profile.CoolDown;
      DamageRate = profile.DamageRate;
      CastWithWeaponSpeed = profile.CastWithWeaponSpeed;
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

      CastTickCount = 0;
      CDTickCount = CDTick;
      if (SkillProfile != null)
      {
        return await SkillProfile.CastSkill(caster, targets, map);
      }
      else
      {
        result.SetSuccess(true);
        return result;
      }

    }
  }
}
