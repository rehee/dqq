using DQQ.Commons;
using DQQ.Components.Items;
using DQQ.Components.Stages;
using DQQ.Components.Stages.Actors.Mobs;
using DQQ.Components.Stages.Maps;
using DQQ.Consts;
using DQQ.Profiles.Skills;
using DQQ.TickLogs;

namespace DQQ.Helper
{
  public static class TickLogHelper
  {
    public static void SetSuccess(this TickLogItem item, int tick, ISkill? skill, ITarget? from, ITarget? to, DamageTaken? damage)
    {
      item.Success = true;
      item.ActionSecond = tick / (decimal)DQQGeneral.TickPerSecond;
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

    public static TickLogActor? ToLogActor(this ITarget? target)
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
      result.MaxLife = target.MaximunLife;
      result.Currentife = target.CurrentHP;
      return result;
    }


    public static void AddMapLog(this IMap map, bool success, ITarget? from, ITarget? to, ISkill? skill, DamageTaken? damage)
    {
      var item = new TickLogItem();
      item.WaveNumber = map.WaveIndex;
      item.Success = success;
      item.ActionSecond = map.PlayingCurrentSecond;
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
      map.Logs.Add(item);
      if (damage?.Drops?.Any() == true)
      {
        map!.Drops!.AddRange(damage!.Drops.ToArray());
      }
      map.XP = map.XP + damage?.XP ?? 0;
    }
    public static void AddMapLogNewWave(this IMap map)
    {
      var item = new TickLogItem();
      map.Logs.Add(item);
      item.WaveNumber = map.WaveIndex;
      item.WaveOrPlayerChange = true;
      item.ActionSecond = map.PlayingCurrentSecond;
      item.Players = map.Players?.Select(b => b!.ToLogActor()!).ToArray();
      item.Enemies = map.MobPool?[map.WaveIndex]?.Select(b => b!.ToLogActor()!).ToArray();
      item.Success = true;
    }

  }
}
