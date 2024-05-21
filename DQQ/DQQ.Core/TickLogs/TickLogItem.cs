using DQQ.Commons;
using DQQ.Components.Skills;
using DQQ.Components.Stages.Actors;
using DQQ.Enums;
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
    public EnumLogType LogType { get; set; }
    public TickLogActor? From { get; set; }
    public TickLogActor? Target { get; set; }
    public TickLogSkill? Skill { get; set; }
    public TicklogDamage? Damage { get; set; }
    public TickLogHealing? Healing { get; set; }
    public bool WaveOrPlayerChange { get; set; }
    public int WaveNumber { get; set; }

    public IEnumerable<TickLogActor>? Players { get; set; }
    public IEnumerable<TickLogActor>? Enemies { get; set; }
  }
}
