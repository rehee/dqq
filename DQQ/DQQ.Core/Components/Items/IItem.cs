using DQQ.Enums;
using DQQ.Profiles.Items;

namespace DQQ.Components.Items;
public interface IItem
{
  Guid? OwnerId { get; }
  int? Quanty { get; }
  int? ItemLevel { get; }
  EnumItem? ItemNumber { get; }
  ItemProfile? ItemProfile { get; }
  Task Use();
}