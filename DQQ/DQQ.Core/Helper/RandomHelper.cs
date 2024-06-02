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
    public static decimal GetRandom(int min, int max = 101, int percentage = 100)
    {
      Random r = new Random(GetRandomSeed());
      if (min <= 0)
      {
        min = 0;
      }
      if (min >= max)
      {
        min = max;
      }

      var randomNumber = r.Next(min, max);
      return randomNumber / (decimal)(max - 1);
    }
    public static int GetRandomInt(int min, int max)
    {
      Random r = new Random(GetRandomSeed());
      return r.Next(min, max < min ? (min + 1) : (max + 1));
    }
    public static T GetRamdom<T>(this IEnumerable<T> enums)
    {
      var array = enums.ToArray();
      Random random = new Random(GetRandomSeed());
      int randomIndex = random.Next(0, array.Length);
      return array[randomIndex];
    }
    public static IEnumerable<T> GetNumberOfRandom<T>(this IEnumerable<T> enums, int numbers)
    {
      Random random = new Random(GetRandomSeed());
      return enums.Select(b => (random.Next(), b)).OrderBy(b => b.Item1).Take(numbers).Select(b => b.b);

    }

    public static long GetRandomRange(this long number, int min = 70, int max = 101)
    {
      var randomNumber = GetRandomInt(min, max);
      return (number * min) / max;
    }
  }
}
