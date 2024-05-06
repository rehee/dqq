using DQQ.Components.Stages.Actors.Characters;
using DQQ.Enums;
using DQQ.Helper;
using DQQ.Pools;
using DQQ.Profiles.Items.Equipments;
using DQQ.UnitTest;

namespace DQQ.Core.UnitTest.Helpers
{
  public class EquipCharTest : BaseTest
  {
    [Test]
    public void EquipItemTest()
    {
      var equipProfile = DQQPool.ItemPool[EnumItem.CorrodedBlade] as EquipProfile;
      var equip = equipProfile!.GenerateEquipComponent(1);
      var player = new Character();
      player.Equip(EnumEquipSlot.MainHand, equip);
      Assert.That(player.MainHand, Is.EqualTo(equip.MainHand));
      Assert.That(player.AttackPerSecond, Is.EqualTo(equip.AttackPerSecond));
    }
  }
}
