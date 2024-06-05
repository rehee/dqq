using DQQ.Components.Items.Equips;
using DQQ.Enums;
using DQQ.Pools;
using DQQ.Profiles.Items;
using DQQ.Profiles.Items.Equipments.OneHandWeapons.OneHandSwords;
using DQQ.UnitTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Core.UnitTest.Profiles.ItemProfiles.EquipProfiles.OneHands.SwordTests
{
  public class OneHandSwardProfileTest : BaseTest
  {

    [Test]
    public void CopperSwordTest()
    {
      var profile = new CopperSword();
      var item = profile.GenerateComponent(1, 1) as EquipComponent;
      Assert.IsNotNull(item?.Property?.AttackRating);

    }

  }
}
