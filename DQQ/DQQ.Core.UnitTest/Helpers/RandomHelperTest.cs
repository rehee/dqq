using DQQ.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.UnitTest.Helpers
{
	public class RandomHelperTest
	{
		[TestCase(1, 100)]
		[TestCase(11, 100)]
		[TestCase(12, 100)]
		[TestCase(13, 100)]
		[TestCase(41, 100)]
		[TestCase(12, 100)]
		[TestCase(81, 100)]
		[TestCase(11, 100)]
		[TestCase(13, 112)]
		[TestCase(14, 110)]
		[TestCase(80, 120)]
		public void RandomPercentageTest(int min, int max)
		{
			var number = RandomHelper.GetRandom(RandomHelper.NewRandom(), min, max);

			Assert.That(number, Is.GreaterThanOrEqualTo(min / max));
			Assert.That(number, Is.LessThanOrEqualTo(max / max));
		}

		[Test]
		public void RamdomSeedTest()
		{
			var seed = RandomHelper.GetRandomSeed();

			var ramdom = new Random(seed);
			var a = ramdom.NextInt64(1, 10);
			var ramdom2 = new Random(seed);
			var b = ramdom2.NextInt64(1, 10);
			Assert.That(a, Is.EqualTo(b));
			for (var i = 0; i < 10000; i++)
			{
				Assert.That(ramdom.NextInt64(), Is.EqualTo(ramdom2.NextInt64()));
			}
		}
	}
}
