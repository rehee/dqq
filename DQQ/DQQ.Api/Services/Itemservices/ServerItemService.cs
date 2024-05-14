using DQQ.Entities;
using DQQ.Enums;
using DQQ.Services.ItemServices;
using Microsoft.EntityFrameworkCore;
using ReheeCmf.Contexts;
using ReheeCmf.Helpers;
using ReheeCmf.Responses;
using DQQ.Helper;

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
    public async Task<ContentResponse<bool>> EquipItem(Guid actorId, Guid itemId, EnumEquipSlot slot)
    {
      var result = new ContentResponse<bool>();
      try
      {
        var item = await context.Query<ItemEntity>(false).Where(b => b.Id == itemId && b.ActorId == actorId).FirstOrDefaultAsync();
        if (item == null || item.EquipType == null)
        {
          result.SetError(System.Net.HttpStatusCode.NotFound);
          return result;
        }
        var avaliableSlots = item.EquipType.GetAvaliableSlots();
        if (avaliableSlots?.Contains(slot) != true)
        {
          result.SetError(System.Net.HttpStatusCode.NotFound);
          return result;
        }

        if (item.EquipType == EnumEquipType.TwoHandWeapon)
        {
          await UnEquipItem(actorId, EnumEquipSlot.MainHand, EnumEquipSlot.OffHand);
        }
        else
        {
          await UnEquipItem(actorId, slot);
        }
        var newEquip = new ActorEquipmentEntity()
        {
          ActorId = actorId,
          ItemId = itemId,
          EquipSlot = slot,
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
    public async Task<ContentResponse<bool>> PickItem(Guid actorId, Guid itemId)
    {
      var result = new ContentResponse<bool>();
      var foundItem = await context.Query<ItemEntity>(true).Where(b => b.Id == itemId).AnyAsync();
      if (foundItem)
      {
        result.SetValidation(ValidationResultHelper.New("Item already picked"));
        return result;

      }
      var item = await tiService.GetTemporaryItems(actorId, itemId);
      if (item == null)
      {
        result.SetError(System.Net.HttpStatusCode.NotFound);
        return result;
      }
      item.ActorId = actorId;
      try
      {
        await context.AddAsync(item);
        await context.SaveChangesAsync();
        result.SetSuccess(true);
      }
      catch (Exception ex)
      {
        result.SetError(ex);

      }
      return result;
    }

    public async Task<ContentResponse<bool>> UnEquipItem(Guid actorId, params EnumEquipSlot[] slots)
    {
      var result = new ContentResponse<bool>();

      if (slots?.Any() != true)
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
