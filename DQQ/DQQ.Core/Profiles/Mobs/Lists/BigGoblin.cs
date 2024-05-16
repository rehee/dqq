using DQQ.Attributes;
using DQQ.Enums;
using System.Numerics;

namespace DQQ.Profiles.Mobs
{
  [Pooled]
  public class BigGoblin : NormalMob
  {
    public override EnumMob ProfileNumber => EnumMob.BigGoblin;
    public override string? Name => "巨型大耳怪";
    public override string? Discription => "";
    public override Int64 Damage => 1;
    public override Int64 HP => 15;
    public override IEnumerable<MobSkill>? Skills => new[] { MobSkill.New(EnumSkill.NormalAttack) };

    public override decimal DropRate => 1m;

    public override Int64 XP => 3;
  }
}
