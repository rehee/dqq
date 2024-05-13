using DQQ.Drops;
using DQQ.Enums;
using DQQ.Profiles.Skills;
using DQQ.XPs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Mobs
{
  public abstract class MobProfile : DQQProfile<EnumMob>, IDropper, IXP
  {
    public abstract Int64 Damage { get; }
    public abstract Int64 HP { get; }
    public abstract IEnumerable<EnumSkill>? Skills { get; }
    public abstract decimal DropRate { get; }
    public abstract Int64 XP { get; }
  }
}
