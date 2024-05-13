using System.Numerics;

namespace DQQ.Profiles.Items.Equipments
{
  public abstract class WeopnProfile : EquipProfile
  {
    public abstract decimal AttackPerSecond { get; }
    public abstract Int64 BaseDamage { get; }
  }
}
