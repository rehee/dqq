using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Commons.DTOs
{
  public class CombatRequestDTO
  {
    public Guid? ActorId { get; set; }
    public int MapLevel { get; set; }
    public int SubMapLevel { get; set; }
  }
}
