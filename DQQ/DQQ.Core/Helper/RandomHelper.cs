using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Helper
{
  public static class RandomHelper
  {
    public static int GetRandomSeed()
    {
      var guid = Guid.NewGuid();
      byte[] bytes = guid.ToByteArray();
      int seed = BitConverter.ToInt32(bytes, 0);
      return seed;
    }
    public static decimal GetRandom(int min, int max = 100)
    {
      Random r = new Random(GetRandomSeed());
      if (min <= 0)
      {
        min = 0;
      }
      if (min >= 100)
      {
        min = 100;
      }
      if (max <= 100)
      {
        max = 100;
      }
      var randomNumber = (decimal)r.Next(min, max);
      return randomNumber / max;
    }

    public static T GetRamdom<T>(this IEnumerable<T> enums)
    {
      var array = enums.ToArray();
      Random random = new Random(GetRandomSeed());
      int randomIndex = random.Next(0, array.Length);
      return array[randomIndex];
    }
  }
}
