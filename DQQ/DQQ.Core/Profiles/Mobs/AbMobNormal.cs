using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Mobs
{
  public abstract class AbMobNormal : AbMobProfile
  {
    public override bool IsBoss => false;
		public override decimal DropRate => 1m;
		public override decimal RarityRaRate => 1m;
	}
}
