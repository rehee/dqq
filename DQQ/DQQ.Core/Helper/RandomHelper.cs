using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Helper
{
	public static class RandomHelper
	{
		public static Random NewRandom()
		{
			return new Random(GetRandomSeed());
		}
		public static int GetRandomSeed(Guid? seedGUid = null)
		{
			var guid = seedGUid ?? Guid.NewGuid();
			byte[] bytes = guid.ToByteArray();
			int seed = BitConverter.ToInt32(bytes, 0);
			return seed;
		}
		public static decimal GetRandom(Random r, int min, int max = 101, int percentage = 100)
		{
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
		public static int GetRandomInt(Random r, int min, int max)
		{

			return r.Next(min, max < min ? (min + 1) : (max + 1));
		}
		public static T GetRamdom<T>(this IEnumerable<T> enums, Random r)
		{
			var array = enums.ToArray();
			int randomIndex = r.Next(0, array.Length);
			return array[randomIndex];
		}
		public static IEnumerable<T> GetNumberOfRandom<T>(this IEnumerable<T> enums, int numbers, Random r)
		{
			return enums.Select(b => (r.Next(), b)).OrderBy(b => b.Item1).Take(numbers).Select(b => b.b);
		}

		public static long GetRandomRange(this long number, Random r, int min = 70, int max = 101)
		{
			var randomNumber = GetRandomInt(r, min, max);
			return (number * min) / max;
		}
	}
}
