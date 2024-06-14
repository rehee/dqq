using DQQ.Commons;
using DQQ.Enums;
using DQQ.Helper;
using DQQ.UnitTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Core.UnitTest.Helpers
{
  public class AffixeHelperTest : BaseTest
  {
    [Test]
    public void AffixeHelper_Drop_Test()
    {
      var change = new AffixeChange()
      {
        ItemLevel = 99,
        EquipType = EnumEquipType.TwoHandWeapon,
        ItemType = EnumItemType.Spear,
        Rarity = EnumRarity.Magic,
      };

      var affect = AffixeHelper.GenerateAllAffies(RandomHelper.NewRandom(), change);
    }
  }
}
