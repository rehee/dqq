﻿using DQQ.Entities;
using DQQ.Enums;
using ReheeCmf.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Services.ItemServices
{
  public interface IItemService
  {
    Task<ContentResponse<bool>> UnEquipItem(Guid? actorId, params EnumEquipSlot[] slots);
    Task<ContentResponse<bool>> PickItem(Guid? actorId, params Guid[] itemId);
    Task<ContentResponse<bool>> EquipItem(Guid? actorId, Guid? itemId, EnumEquipSlot? slot = null);
    Task<IEnumerable<ItemEntity>?> PickableItems(Guid? actorId);
    Task<IEnumerable<ItemEntity>?> ActorInventory(Guid? actorId);
  }
}