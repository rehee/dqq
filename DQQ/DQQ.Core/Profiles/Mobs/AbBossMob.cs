using DQQ.Components.Stages.Actors;
using DQQ.Components.Stages.Actors.Mobs;
using DQQ.Components.Stages.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Mobs
{
  public abstract class MobBoss : MobProfile
  {
    public override bool IsBoss => true;
		public override decimal DropRate => 1m;
		public override decimal RarityRaRate => 1m;
		public abstract List<IActor> GenerateBossFight(IMap map);
	}
}
