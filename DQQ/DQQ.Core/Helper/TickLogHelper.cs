using DQQ.Commons;
using DQQ.Components.Items;
using DQQ.Components.Parameters;
using DQQ.Components.Stages;
using DQQ.Components.Stages.Actors.Mobs;
using DQQ.Components.Stages.Maps;
using DQQ.Consts;
using DQQ.Enums;
using DQQ.Profiles;
using DQQ.Profiles.Durations;
using DQQ.Profiles.Skills;
using DQQ.TickLogs;
using static System.Net.Mime.MediaTypeNames;

namespace DQQ.Helper
{
  public static class TickLogHelper
  {

    public static void SetSuccess(this TickLogItem item, int tick, SkillProfile? skill, ITarget? from, ITarget? to, DamageTaken? damage)
    {
      item.Success = true;
      item.ActionSecond = tick / (decimal)DQQGeneral.TickPerSecond;
      item.ActionTick = tick;
      item.From = from.ToLogActor();
      item.Target = to.ToLogActor();
      item.Damage = new TicklogDamage
      {
        DamagePoint = damage?.DamagePoint.ToString(),
        IsKilled = damage?.IsKilled == true
      };
      item.Skill = skill == null ? null : new TickLogSkill
      {
        SkillName = skill.SkillName,
        SkillNumber = skill.SkillNumber,
      };

    }
    public static TickLogActor? ToLogActor(this ITarget? target, bool withDuration = false)
    {
      if (target == null)
      {
        return null;
      }
      var result = new TickLogActor();
      result.DisplayName = target.DisplayName;
      if (target is Monster mb)
      {
        result.MobNumber = mb.MobNumber;
      }
      result.Id = target.DisplayId;
      result.PowerLevel = target.PowerLevel;
      result.MaxLife = target?.CombatPanel?.DynamicPanel.MaximunLife;
      result.Currentife = target?.CurrentHP;
      if (withDuration)
      {
        result.Durations = target?.Durations?.Select(b => TickLogDuration.New(b)).Where(b => b != null).Select(b => b!).ToArray();
      }
      return result;
    }
    public static TickLogItem GetTickLogItemFromMap(this IMap? map, bool success)
    {
      var item = new TickLogItem();
      item.WaveNumber = map?.WaveIndex ?? -1;
      item.Success = success;
      item.ActionTick = map?.TickCount ?? -1;
      item.ActionSecond = (map?.TickCount ?? -1) / (decimal)DQQGeneral.TickPerSecond;
      item.Players = map?.Players?.Select(b => b!.ToLogActor(true)!).ToArray();
      if (map?.WaveIndex >= 0)
      {
        item.Enemies = map?.MobPool?[map.WaveIndex]?.Select(b => b!.ToLogActor(true)!).ToArray();
      }
      return item;
    }

    public static void AddMapLogSpillCast(this IMap map, bool success, ITarget? from, ITarget? to, SkillProfile? skill)
    {
      var item = map.GetTickLogItemFromMap(success);
      item.LogType = EnumLogType.CastSkill;

      item.From = from.ToLogActor();
      item.Target = to.ToLogActor();
      item.Skill = skill == null ? null : new TickLogSkill
      {
        SkillName = skill.SkillName,
        SkillNumber = skill.SkillNumber,
      };
      map?.Logs?.Add(item);
    }

    public static void AddMapLogNewWave(this IMap map)
    {
      var item = map.GetTickLogItemFromMap(true);
			item.LogType = EnumLogType.WaveChange;
			map?.Logs?.Add(item);
    }
    public static void AddMapLogHealingTaken(this IMap? map, bool success, ITarget? from, ITarget? to, DQQProfile? profile, TickLogHealing healing)
    {
      var item = map.GetTickLogItemFromMap(success);
      item.LogType = EnumLogType.HealingTaken;
      item.From = from.ToLogActor();
      item.Target = to.ToLogActor();
      item.Healing = healing;
      item.SetLogProfile(profile);
      if (map != null)
      {
        map.Logs.Add(item);
      }

    }
    public static void AddMapLogDamageTaken(this IMap? map, ComponentTickParameter parameter)
    {
      var item = map.GetTickLogItemFromMap(parameter.Damage?.DamageTakenSuccess == true);
      item.LogType = EnumLogType.DamageTaken;


      item.From = parameter.From.ToLogActor();
      item.Target = parameter.To.ToLogActor();
      item.SetLogProfile(parameter.Source);
      if (parameter.Damage != null)
      {
        item.Damage = new TicklogDamage
        {
          DamagePoint = parameter.Damage.DamagePoint.ToString(),
          IsKilled = parameter.Damage.IsKilled == true,
          HitCheck = parameter.Damage.HitCheck
				};
      }

      if (map != null)
      {
        if (parameter.Damage?.Drops?.Any() == true)
        {
          map!.Drops!.AddRange(parameter.Damage!.Drops.ToArray());
        }
        map.XP = map.XP + (parameter.Damage?.XP ?? 0);
        map.Logs.Add(item);
      }

    }

    public static ProfileSource? GetProfileSource(this DQQProfile? profile)
    {
      if (profile == null)
      {
        return null;
      }
      if (profile is SkillProfile sp)
      {
        return new ProfileSource
        {
          ProfileFromSkill = sp
        };
      }
      if (profile is DurationProfile dp)
      {
        return new ProfileSource
        {
          ProfileFromDuration = dp
        };
      }

      return null;
    }
    public static void SetLogProfile(this TickLogItem? item, ProfileSource? source)
    {
      if (item == null || source == null)
      {
        return;
      }
      if (source?.ProfileFromSkill != null)
      {
        item.Skill = new TickLogSkill
        {
          SkillName = source?.ProfileFromSkill.SkillName,
          SkillNumber = source?.ProfileFromSkill.SkillNumber,
        };
      }
      if (source?.ProfileFromDuration != null)
      {
        item.Skill = new TickLogSkill
        {
          SkillName = source?.ProfileFromDuration.Name,
          DurationNumber = source?.ProfileFromDuration.ProfileNumber,
        };
      }
    }

    public static void SetLogProfile(this TickLogItem? item, DQQProfile? source)
    {
      item.SetLogProfile(source.GetProfileSource());
    }
  }
}
