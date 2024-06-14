using DQQ.Helper;
using DQQ.UnitTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Core.UnitTest.Helpers
{
	public class IncreaseHelperTest : BaseTest
	{
		[TestCase(1, 6)]
		[TestCase(70, 200)]
		public void WeaponDamagePersecondTest(int weaponLevel, long damage)
		{
			var actual = weaponLevel.WeponDamageIncrease(null);

			Assert.That(actual, Is.EqualTo(damage));
		}
	}
}
