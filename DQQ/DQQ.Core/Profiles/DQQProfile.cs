using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles
{
  public abstract class DQQProfile<T> where T : Enum
  {
    public abstract T ProfileNumber { get; }
    public abstract string? Name { get; }
    public abstract string? Discription { get; }
  }
}
