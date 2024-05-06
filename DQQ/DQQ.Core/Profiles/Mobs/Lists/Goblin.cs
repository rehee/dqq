using DQQ.Attributes;
using DQQ.Enums;
using System.Numerics;

namespace DQQ.Profiles.Mobs
{
  [Pooled]
  public class Goblin : MobProfile
  {
    public override EnumMob ProfileNumber => EnumMob.Goblin;

    public override string? Name => "大耳怪";

    public override string? Discription => "大耳怪";

    public override BigInteger Damage => 1;

    public override BigInteger HP => 10;

    public override IEnumerable<EnumSkill>? Skills => new[] { EnumSkill.NormalAttack };

    public override decimal DropRate => 1m;

    public override BigInteger XP => 2;
  }
}
