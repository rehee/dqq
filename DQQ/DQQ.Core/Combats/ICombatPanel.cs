using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Combats
{
  public interface ICombatPanel
  {
    ICombatProperty StaticPanel { get; }
    ICombatProperty DynamicPanel { get; }
    void CalculateDynamicPanel();
  }
}
