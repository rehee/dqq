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
    DamageTaken BeforeTakeDamage(BigInteger damage, ITarget attacker, IMap? map);
    DamageTaken TakeDamage(BigInteger damage, ITarget attacker, IMap? map);
    DamageTaken AfterTakeDamage(BigInteger damage, ITarget attacker, IMap? map);
  }
}