using DQQ.Components;
using DQQ.Components.Items;
using DQQ.Enums;
using System.ComponentModel;

namespace DQQ.Combats
{
  public interface IEquptment : IItem, IDQQComponent
  {
    EnumEquipType? EquipType { get; set; }
    EnumEquipSlot? EquipSlot { get; }
    EnumEquipSlot? SecondEquipSlot { get; }
    CombatProperty? Property { get; set; }
  }
}
