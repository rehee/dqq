using DQQ.Commons;
using DQQ.Components.Skills;
using DQQ.Components.Stages.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.TickLogs
{
  public class TickLogItem
  {
    public static TickLogItem New() => new TickLogItem();
    public bool Success { get; set; }
    public decimal? ActionSecond { get; set; }
    public TickLogActor? From { get; set; }
    public TickLogActor? Target { get; set; }
    public TickLogSkill? Skill { get; set; }
    public TicklogDamage? Damage { get; set; }
  }
}
