using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DQQ.TickLogs
{
  public class TickLogActor
  {
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Guid? Id { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public EnumMob? MobNumber { get; set; }
    public string? DisplayName { get; set; }
    public long? MaxLife { get; set; }
    public long? Currentife { get; set; }
    public double PercentageHP => ((MaxLife == null || MaxLife == 0 || Currentife <= 0) ? 0d : ((Currentife ?? 0d) / (double)MaxLife.Value)) * 100;
    public int PercentageHPInt => (int)Math.Ceiling(PercentageHP);
  }
}
