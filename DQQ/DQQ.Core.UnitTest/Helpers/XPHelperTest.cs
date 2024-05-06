using DQQ.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Core.UnitTest.Helpers
{
  public class XPHelperTest
  {
    [TestCase(10)]
    [TestCase(101)]
    [TestCase(120)]
    [TestCase(410)]
    [TestCase(130)]
    public void NextXPTest2(int y)
    {
      var level10Xp = XPHelper.GetNextLevelXP(y);
      var level11Xp = XPHelper.GetNextLevelXP(y + 1, level10Xp, y);
      var level11xp2 = XPHelper.GetNextLevelXP(y + 1);
      Assert.That(level11Xp, Is.EqualTo(level11xp2));
    }
  }
}
