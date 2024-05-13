using DQQ.Commons;
using DQQ.Components.Stages;
using DQQ.Components.Stages.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Components
{
  public interface ITakeDamage
  {
    DamageTaken BeforeTakeDamage(Int64 damage, ITarget attacker, IMap? map);
    DamageTaken TakeDamage(Int64 damage, ITarget attacker, IMap? map);
    DamageTaken AfterTakeDamage(Int64 damage, ITarget attacker, IMap? map);
  }
}