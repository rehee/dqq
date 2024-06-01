﻿using DQQ.Components.Items.Equips;
using DQQ.Enums;
using DQQ.Profiles.Items.Equipments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Commons
{
  public class AffixeChange
  {
    public static AffixeChange New(AffixeChange changes)
    {
      return new AffixeChange
      {
        EquipType = changes.EquipType,
        IsPrefix = changes.IsPrefix,
        ItemLevel = changes.ItemLevel,
        ItemType = changes.ItemType,
        Rarity = changes.Rarity
      };
    }
    public static AffixeChange New(EquipComponent equip)
    {
      return new AffixeChange
      {
        EquipType = equip.EquipType.Value,
        ItemLevel = equip.ItemLevel ?? 0,
        ItemType = equip.ItemType,
        Rarity = equip.Rarity
      };
    }
    public bool IsPrefix { get; set; }
    public int ItemLevel { get; set; }
    public EnumRarity Rarity { get; set; }
    public EnumEquipType EquipType { get; set; }
    public EnumItemType ItemType { get; set; }
  }
}
