using DQQ.Attributes;
using DQQ.Enums;
using System.Numerics;

namespace DQQ.Profiles.Mobs
{
  [Pooled]
  public class BigGoblin : MobProfile
  {
    public override EnumMob ProfileNumber => EnumMob.BigGoblin;
    public override string? Name => "巨型大耳怪";
    public override string? Discription => "";
    public override BigInteger Damage => 1;
    public override BigInteger HP => 15;
    public override IEnumerable<EnumSkill>? Skills => new[] { EnumSkill.NormalAttack };

    public override decimal DropRate => 1m;

    public override BigInteger XP => 3;
  }
}
