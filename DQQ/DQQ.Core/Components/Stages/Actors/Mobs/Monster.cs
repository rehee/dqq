using DQQ.Commons;
using DQQ.Components.Skills;
using DQQ.Components.Stages.Maps;
using DQQ.Consts;
using DQQ.Drops;
using DQQ.Enums;
using DQQ.Helper;
using DQQ.Profiles.Mobs;
using DQQ.XPs;
using System.Numerics;

namespace DQQ.Components.Stages.Actors.Mobs
{
  public class Monster : Actor, IDropper, IXP
  {
    public EnumMob MobNumber { get; set; }
    public EnumMobRarity Rarity { get; set; } = EnumMobRarity.Normal;
    public decimal DropRate { get; set; }
    public BigInteger XP { get; set; }
    public static Monster Create(MobProfile profile, int level, EnumMobRarity rarity)
    {
      var mob = new Monster();
      var namePrefix = "";
      var dropTimes = 1;
      switch (rarity)
      {
        case EnumMobRarity.Magic:
          namePrefix = "Magic";
          break;
        case EnumMobRarity.Champion:
          dropTimes = 3;
          namePrefix = "Champion";
          break;
        case EnumMobRarity.Boss:
          namePrefix = "Boss";
          dropTimes = 5;
          break;
      }
      mob.MobNumber = profile.ProfileNumber;
      mob.Alive = true;
      mob.Targetable = true;
      mob.Rarity = rarity;
      mob.DisplayName = $"{namePrefix} {profile.Name}";
      mob.BasicDamage = DQQGeneral.MobStatusCalculate(level, profile.Damage, rarity);
      mob.MaximunLife = DQQGeneral.MobStatusCalculate(level, profile.HP, rarity);
      mob.XP = DQQGeneral.MobStatusCalculate(level, profile.XP, rarity);
      mob.CurrentHP = mob.MaximunLife ?? 0;
      mob.DropRate = profile.DropRate * dropTimes;
      if (profile?.Skills?.Any() == true)
      {
        mob.Skills = new Dictionary<int, ISkillComponent?>();
        var skillCount = 0;
        foreach (var skill in profile.Skills)
        {
          mob.Skills.TryAdd(skillCount, SkillComponent.New(skill));
          skillCount++;
        }
      }
      return mob;
    }

    public override DamageTaken TakeDamage(ITarget? from, BigInteger damage, IMap? map)
    {
      var result = base.TakeDamage(from, damage, map);

      if (result.IsKilled)
      {
        result.Drops = DropHelper.Drop(this, map);
        result.XP = XP;
      }

      return result;
    }
  }
}
