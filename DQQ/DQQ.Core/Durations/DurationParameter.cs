using DQQ.Components.Stages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Durations
{
  public class DurationParameter
  {
    public ITarget? Creator { get; set; }
    public decimal DurationSeconds { get; set; }
    public long Value { get; set; }
  }
}
