using DQQ.Components.Items.Equips;
using DQQ.Enums;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Combats
{
  public interface IEquippableCharacter : IWIthCombatPanel
  {
    ConcurrentDictionary<EnumEquipSlot, IEquptment?> Equips { get; set; }
  }
}
