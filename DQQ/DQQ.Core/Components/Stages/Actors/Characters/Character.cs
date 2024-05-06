using DQQ.Combats;
using DQQ.Commons;
using DQQ.Components.Stages.Maps;
using DQQ.Enums;
using System.Collections.Concurrent;
using System.Numerics;

namespace DQQ.Components.Stages.Actors.Characters
{
  public class Character : Actor, IEquippableCharacter
  {
    public Character()
    {
      Equips = new ConcurrentDictionary<EnumEquipSlot, IEquptment?>();
    }
    public BigInteger CurrentXP { get; set; }
    public BigInteger NextLevelXP { get; set; }
    public ConcurrentDictionary<EnumEquipSlot, IEquptment?> Equips { get; set; }

    public override DamageTaken TakeDamage(ITarget? from, BigInteger damage, IMap? map)
    {
      var result = base.TakeDamage(from, damage, map);

      if (result.IsKilled)
      {

      }
      return result;
    }



  }
}
