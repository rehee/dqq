using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Helper
{
  public static class ValuesHelper
  {
    public static long DefaultValue(this long? input, long defaultValue = 0)
    {
      return input == null ? defaultValue : input.Value;
    }
    public static decimal DefaultValue(this decimal? input, decimal defaultValue = 0)
    {
      return input == null ? defaultValue : input.Value;
    }
    public static int DefaultValue(this int? input, int defaultValue = 0)
    {
      return input == null ? defaultValue : input.Value;
    }
  }
}
