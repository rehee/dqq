using DQQ.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.UnitTest.Consts
{
  public class GeneralTest
  {
    [TestCase(1, 1, 1)]
    [TestCase(10, 1, 1)]
    [TestCase(15, 1, 1)]
    [TestCase(100, 1, 5)]
    [TestCase(150, 1, 8)]
    [TestCase(3, 100, 110)]
    [TestCase(4, 100, 115)]
    public void GeneralMobStateIncreasedTest(int mobLevel, int value, int expected)
    {
      var actual = DQQGeneral.MobStatusCalculate(mobLevel, value);
      Assert.That(actual, Is.EqualTo(new BigInteger(expected)));
    }
  }
}
