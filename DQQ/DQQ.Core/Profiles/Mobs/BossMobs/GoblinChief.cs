using DQQ.Attributes;
using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Mobs.BossMobs
{
  [Pooled]
  public class GoblinChief : BossMob
  {
    public override long Damage => 5;

    public override long HP => 50;

    public override IEnumerable<MobSkill>? Skills => new[]
    {
      MobSkill.New(EnumSkill.NormalAttack),
      MobSkill.New(EnumSkill.HatefulStrike,
        new Strategies.SkillStrategies.SkillStrategy
        {
          Priority=0,
          Condition=true,
          Property= EnumPropertyCompare.HealthPercentage,
          Compare= EnumCompare.LessThan,
          Value = 0.55m,
        })
    };

    public override decimal DropRate => 0.15m;

    public override long XP => 20;

    public override EnumMob ProfileNumber => EnumMob.GoblinChief;

    public override string? Name => "哥布林酋长";

    public override string? Discription => "哥布林酋长";
  }
}
