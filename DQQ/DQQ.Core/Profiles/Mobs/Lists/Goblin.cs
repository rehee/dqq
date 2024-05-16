﻿using DQQ.Attributes;
using DQQ.Enums;
using System.Numerics;

namespace DQQ.Profiles.Mobs
{
  [Pooled]
  public class Goblin : NormalMob
  {
    public override EnumMob ProfileNumber => EnumMob.Goblin;

    public override string? Name => "大耳怪";

    public override string? Discription => "大耳怪";

    public override Int64 Damage => 1;

    public override Int64 HP => 10;

    public override IEnumerable<MobSkill>? Skills => new[] { MobSkill.New(EnumSkill.NormalAttack) };

    public override decimal DropRate => 1m;

    public override Int64 XP => 2;
  }
}
