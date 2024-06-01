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
    public static AffixeRange New(EnumPropertyType PropertyType, int? Min = null, int? Max = null)
    {
      var result = new AffixeRange();
      result.PropertyType = PropertyType;

      result.Min = Min.GetValueOrDefault(1);
      result.Max = Max.GetValueOrDefault(result.Min) < result.Min ? result.Min : result.Max;

      return result;
    }
    public EnumPropertyType PropertyType { get; set; }
    public int Min { get; set; }
    public int Max { get; set; }
    
    public AffixPower NewPower()
    {
      return new AffixPower
      {
        PropertyType = PropertyType,
        Power = RandomHelper.GetRandomInt(Min, Max)
      };
    }
  }
}
