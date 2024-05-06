using DQQ.Components;
using DQQ.Components.Items;
using DQQ.Enums;
using System.ComponentModel;

namespace DQQ.Combats
{
  public interface IEquptment : ICombatProperty, IItem, IDQQComponent
  {
    EnumEquipType EquipType { get; set; }
    EnumEquipSlot? EquipSlot { get; }
    EnumEquipSlot? SecondEquipSlot { get; }
  }
}
