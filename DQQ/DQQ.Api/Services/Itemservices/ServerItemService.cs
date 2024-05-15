using DQQ.Entities;
using DQQ.Enums;
using DQQ.Services.ItemServices;
using Microsoft.EntityFrameworkCore;
using ReheeCmf.Contexts;
using ReheeCmf.Helpers;
using ReheeCmf.Responses;
using DQQ.Helper;
using System.Linq;
using DQQ.Pools;
using Microsoft.Linq.Translations;

namespace DQQ.Api.Services.Itemservices
{
  public class ServerItemService : IItemService
  {
    private readonly IContext context;
    private readonly ITemporaryService tiService;

    public ServerItemService(IContext context, ITemporaryService tiService)
    {
      this.context = context;
      this.tiService = tiService;
    }

    public async Task<IEnumerable<ItemEntity>?> ActorInventory(Guid? actorId)
    {
      await Task.CompletedTask;
      if (actorId == null)
      {
        return null;
      }
      return await context.Query<ItemEntity>(true).Where(b => b.ActorId == actorId && b.IsEquipped != true).WithTranslations().ToArrayAsync();
    }

    public async Task<ContentResponse<bool>> EquipItem(Guid? actorId, Guid? itemId, EnumEquipSlot? slot)
    {
      var result = new ContentResponse<bool>();
      try
      {
        if (actorId == null || itemId == null)
        {
          return result;
        }
        var item = await context.Query<ItemEntity>(false).Where(b => b.Id == itemId && b.ActorId == actorId).FirstOrDefaultAsync();
        if (item == null || item.EquipType == null)
        {
          result.SetError(System.Net.HttpStatusCode.NotFound);
          return result;
        }
        var avaliableSlots = item.EquipType.GetAvaliableSlots();
        EnumEquipSlot equipSlot;
        if (slot == null)
        {
          equipSlot = avaliableSlots.FirstOrDefault();
        }
        else
        {
          if (avaliableSlots?.Contains(slot.Value) != true)
          {
            result.SetError(System.Net.HttpStatusCode.NotFound);
            return result;
          }
          equipSlot = slot.Value;
        }



        if (item.EquipType == EnumEquipType.TwoHandWeapon)
        {
          await UnEquipItem(actorId.Value, EnumEquipSlot.MainHand, EnumEquipSlot.OffHand);
        }
        else
        {
          await UnEquipItem(actorId.Value, equipSlot);
        }
        var newEquip = new ActorEquipmentEntity()
        {
          ActorId = actorId,
          ItemId = itemId,
          EquipSlot = equipSlot,
        };
        await context.AddAsync(newEquip);
        await context.SaveChangesAsync();
        result.SetSuccess(true);

      }
      catch (Exception ex)
      {
        result.SetError(ex);
      }

      return result;
    }

    public async Task<IEnumerable<ItemEntity>?> PickableItems(Guid? actorId)
    {
      if (actorId == null)
      {
        return null;
      }
      var items = await tiService.GetAllTemporaryItems(actorId);
      var itemIds = items.Select(b => b.Id).ToHashSet();
      var itemsAlreadyExisting = await context.Query<ItemEntity>(true).Where(b => itemIds.Contains(b.Id)).Select(b => b.Id).ToArrayAsync();
      var itemNeedRemove = items.Where(b => itemsAlreadyExisting.Contains(b.Id)).Select(b => b.Id).ToArray();
      await tiService.PickAndRemoveTemporaryItems(actorId, itemNeedRemove);
      return items.Where(b => !itemsAlreadyExisting.Contains(b.Id)).ToArray();
    }

    public async Task<ContentResponse<bool>> PickItem(Guid? actorId, params Guid[] itemId)
    {
      var result = new ContentResponse<bool>();
      if (actorId == null || itemId?.Any() != true)
      {
        return result;
      }
      var foundItem = await context.Query<ItemEntity>(true).Where(b => itemId.Any(i => b.Id == i)).AnyAsync();
      if (foundItem)
      {
        result.SetValidation(ValidationResultHelper.New("One of Item already picked"));
        return result;

      }
      var items = await tiService.PickAndRemoveTemporaryItems(actorId, itemId);
      if (items?.Any() == null)
      {
        result.SetError(System.Net.HttpStatusCode.NotFound);
        return result;
      }

      foreach (var item in items.GroupBy(b => b.ItemNumber))
      {
        var itemProfile = DQQPool.ItemPool[item.Key];
        if (itemProfile.IsStack)
        {
          var sum = item.Sum(b => b.Quantity ?? 0);
          if (sum == 0)
          {
            continue;
          }
          var existingItem = await context.Query<ItemEntity>(false).Where(b => b.ActorId == actorId && b.ItemNumber == item.Key).FirstOrDefaultAsync();
          if (existingItem != null)
          {
            existingItem.Quantity = existingItem.Quantity + sum;
          }
          else
          {
            var first = item.FirstOrDefault();
            first.Quantity = sum;
            first.ActorId = actorId;
            await context.AddAsync(first);
          }
        }
        else
        {
          foreach (var subItem in item)
          {
            subItem.ActorId = actorId;
            await context.AddAsync(subItem);
          }
        }
      }
      try
      {
        await context.SaveChangesAsync();
        result.SetSuccess(true);
      }
      catch (Exception ex)
      {
        result.SetError(ex);

      }
      return result;
    }
    public async Task<ContentResponse<bool>> UnEquipItem(Guid? actorId, params EnumEquipSlot[] slots)
    {
      var result = new ContentResponse<bool>();

      if (actorId == null || slots?.Any() != true)
      {
        result.SetSuccess(true);
        return result;
      }

      var list = slots.ToArray();
      var existingEquip = await context.Query<ActorEquipmentEntity>(false).Where(b =>
            b.ActorId == actorId && (b.EquipSlot == null || slots.Contains(b.EquipSlot.Value)))
          .ToArrayAsync();
      foreach (var e in existingEquip)
      {
        context.Delete<ActorEquipmentEntity>(e);
      }
      await context.SaveChangesAsync();
      result.SetSuccess(true);

      return result;
    }

  }
}
