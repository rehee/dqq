using DQQ.Entities;
using System.Numerics;

namespace DQQ.UnitTest.Profiles
{
  public class ActorProfileTest : BaseTest
  {
    [Test]
    public async Task ActorProfileTest_Generate_Test_Happy_Path()
    {
      var actorProfile = new ActorEntity()
      {
        Id = Guid.NewGuid(),
        Name = "Actor1",
        MaxHP = 100,
        BasicDamage = 10,
      };

      var skillProfile = new SkillEntity()
      {
        Id = Guid.NewGuid(),
        Name = "Skill1",
        Slot = 0,
        SkillNumber = Enums.EnumSkill.NormalAttack
      };
      actorProfile.Skills = new List<SkillEntity> { skillProfile };
      var actor = actorProfile.GenerateComponent();
      Assert.That(actor.DisplayId, Is.EqualTo(actorProfile.Id));
      Assert.That(actor.DisplayName, Is.EqualTo(actorProfile.Name));
      Assert.That(actor.CombatPanel.DynamicPanel.MaximunLife, Is.EqualTo(actorProfile.MaxHP));
      Assert.That(actor.CurrentHP, Is.EqualTo(actorProfile.MaxHP));
      Assert.That(actor.BasicDamage, Is.EqualTo(actorProfile.BasicDamage));
      Assert.That(actor.Skills!.FirstOrDefault()!.DisplayId, Is.EqualTo(skillProfile.Id));
      Assert.That(actor.Skills!.FirstOrDefault()!.DisplayName, Is.EqualTo(skillProfile.Name));
    }
  }
}
