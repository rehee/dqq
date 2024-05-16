using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Mobs
{
  public abstract class BossMob : MobProfile
  {
    public override bool IsBoss => true;
  }
}
