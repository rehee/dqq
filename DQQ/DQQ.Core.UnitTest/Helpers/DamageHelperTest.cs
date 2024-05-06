using DQQ.Helper;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.UnitTest.Helpers
{
  public class DamageHelperTest
  {
    [TestCase("1", 0.5, "0")]
    [TestCase("1", 0.6, "0")]
    [TestCase("10", 0.5, "5")]
    [TestCase("10", 0.6, "6")]
    [TestCase("100", 0.5, "50")]
    [TestCase("100", 0.6, "60")]
    [TestCase("100", 0.601, "60")]
    public void BigintPercentageTest(string input, decimal percentage, string expected)
    {
      var inputBig = BigInteger.Parse(input);
      var expectBig = BigInteger.Parse(expected);

      var actual = DamageHelper.Percentage(inputBig, percentage);

      Assert.That(actual, Is.EqualTo(expectBig));
    }
  }
}
