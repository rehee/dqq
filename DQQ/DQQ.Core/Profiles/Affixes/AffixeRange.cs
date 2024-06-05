using DQQ.Combats;
using DQQ.Enums;
using DQQ.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Affixes
{
  public class AffixeRange
  {
    public static AffixeRange New(EnumPropertyType PropertyType, int? Min = null, int? Max = null, EnumAffixeRangeType rangeType = EnumAffixeRangeType.FixedRange,
      decimal pointPerLevel = 1m, double basePower = 0, double power = 0, double powerPercent = 1
      )
    {
      var result = new AffixeRange();
      result.PropertyType = PropertyType;
      result.RangeType = rangeType;
      result.Min = Min.GetValueOrDefault(1);
      result.Max = Math.Max(Max.GetValueOrDefault(result.Min), result.Min);
      result.PointPerLevel = pointPerLevel;
      result.BasePower = basePower;
      result.Power = power;
      result.PowerPercent = powerPercent;

      return result;
    }
    public EnumPropertyType PropertyType { get; set; }
    public int Min { get; set; }
    public int Max { get; set; }
    public decimal PointPerLevel { get; set; }
    public double BasePower { get; set; }
    public double Power { get; set; }
    public double PowerPercent { get; set; }
    public EnumAffixeRangeType RangeType { get; set; }

    public AffixPower NewPower(int level = 1)
    {
      int powers = 0;
      switch (RangeType)
      {
        case EnumAffixeRangeType.LevelBased:
          powers = (int)Math.Round(level * PointPerLevel, 0);
          break;

        case EnumAffixeRangeType.PowerFunction:
          powers = (int)(BasePower * Math.Pow(level, powers) * PowerPercent);
          break;
        case EnumAffixeRangeType.FixedRange:
          powers = RandomHelper.GetRandomInt(Min, Max);
          break;
      }
      powers = (int)RandomHelper.GetRandomRange(powers);
      powers = Math.Max(Min, powers);
      powers = Math.Min(Max, powers);
      return new AffixPower
      {
        PropertyType = PropertyType,
        Power = powers
      };
    }
  }
}
