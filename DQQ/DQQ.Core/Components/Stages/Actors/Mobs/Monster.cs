using DQQ.Commons;
using DQQ.Components.Skills;
using DQQ.Components.Stages.Maps;
using DQQ.Consts;
using DQQ.Drops;
using DQQ.Enums;
using DQQ.Helper;
using DQQ.Profiles;
using DQQ.Profiles.Mobs;
using DQQ.TickLogs;
using DQQ.XPs;
using System.Numerics;

namespace DQQ.Components.Stages.Actors.Mobs
{
  public class Monster : Actor, IDropper, IXP
  {
    public EnumMob MobNumber { get; set; }
    public EnumMobRarity Rarity { get; set; } = EnumMobRarity.Normal;
    public decimal DropRate { get; set; }
    public override EnumTargetLevel PowerLevel
    {
      get
      {
        if (Profile is MobProfile mp)
        {
          if (mp.IsBoss)
          {
            return EnumTargetLevel.Guardian;
          }
        }
        switch (Rarity)
        {
          case EnumMobRarity.Normal:
            return EnumTargetLevel.Normal;
          case EnumMobRarity.Magic:
            return EnumTargetLevel.Magic;
          case EnumMobRarity.Elite:
            return EnumTargetLevel.Elite;
          case EnumMobRarity.Champion:
            return EnumTargetLevel.Champion;
        }
        return base.PowerLevel;
      }
    }
    public Int64 XP { get; set; }
    public static Monster Create(MobProfile profile, int level, EnumMobRarity? rarity = null)
    {
      var mob = new Monster();
      mob.Profile = profile;
      var namePrefix = "";
      var dropTimes = 1;
      if (profile.IsBoss != true)
      {
        switch (rarity)
        {
          case EnumMobRarity.Magic:
            namePrefix = "Magic";
            dropTimes = 2;
            break;
          case EnumMobRarity.Elite:
            namePrefix = "Elite";
            dropTimes = 4;
            break;
          case EnumMobRarity.Champion:
            dropTimes = 8;
            namePrefix = "Champion";
            break;


        }
      }


      mob.MobNumber = profile.ProfileNumber;
      mob.Alive = true;
      mob.Targetable = true;
      mob.Rarity = rarity ?? EnumMobRarity.Normal;
      mob.DisplayName = $"{namePrefix} {profile.Name}";
      mob.BasicDamage = DQQGeneral.MobStatusCalculate(level, profile.Damage, rarity, profile.IsBoss);
      mob.CombatPanel.StaticPanel.MaximunLife = DQQGeneral.MobStatusCalculate(level, profile.HP, rarity, profile.IsBoss);
      mob.XP = DQQGeneral.MobStatusCalculate(level, profile.XP, rarity, profile.IsBoss);
      mob.CurrentHP = mob.CombatPanel.StaticPanel.MaximunLife ?? 0;
      mob.DropRate = profile.DropRate * dropTimes;
      if (profile?.Skills?.Any() == true)
      {
        var list = new List<ISkillComponent>();
        var skillCount = 0;
        foreach (var skill in profile.Skills)
        {
          list.Add(SkillComponent.New(skill, skillCount));
          skillCount++;
        }
        mob.Skills = list.ToArray();
      }
      return mob;
    }

    protected override void AfterTakeDamage(ITarget? from, DamageTaken damage, IMap? map, IDQQProfile? source)
    {
      base.AfterTakeDamage(from, damage, map, source);
      if (!damage.DamageTakenSuccess)
      {
        return;
      }
      if (!damage.IsKilled)
      {
        return;
      }
      damage.Drops = DropHelper.Drop(this, map);
      damage.XP = XP;
    }
  }
}
