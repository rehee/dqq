using DQQ.Helper;
using DQQ.UnitTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Core.UnitTest.Helpers
{
	public class ExpHelperTest : BaseTest
	{
		[TestCase(0, "150")]
		[TestCase(-1, "150")]
		[TestCase(1, "150")]
		[TestCase(69, "583407682")]
		[TestCase(70, "583407682")]
		[TestCase(71, "583407682")]
		public void GainNextXPTest(int currentLevel, string xpExpected)
		{
			var actual = XPHelper.GetNextLevelUpExp(currentLevel);
			var expected = BigInteger.Parse(xpExpected);
			Assert.That(actual, Is.EqualTo(expected));
		}
		[TestCase(1, "1", 1, "1")]
		[TestCase(1, "150", 2, "0")]
		[TestCase(1, "151", 2, "1")]
		[TestCase(1, "400", 3, "62")]
		public void CheckLevelUpTest(int currentLevel, string xp, int levelExpected, string xpExpected)
		{
			var parameter = ExperienceAndLevel.New(currentLevel, BigInteger.Parse(xp));
			var result = XPHelper.CheckExperienceAndLevelUP(parameter);
			Assert.That(result.Level, Is.EqualTo(levelExpected));
			Assert.That(result.Experience, Is.EqualTo(BigInteger.Parse(xpExpected)));
		}

		[TestCase(1, "8")]
		[TestCase(2, "8")]
		[TestCase(3, "8")]
		[TestCase(70, "231")]
		public void MobExpCalculateTest(int level, long exp)
		{
			Assert.That(XPHelper.GetMobKilledExp(level), Is.EqualTo(exp));
		}
	}
}
