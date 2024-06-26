﻿using DQQ.Entities;
using DQQ.Enums;
using DQQ.Services.ItemServices;
using Microsoft.AspNetCore.Mvc;
using ReheeCmf.Authenticates;
using ReheeCmf.Modules.Controllers;

namespace DQQ.Api.Apis
{
	[ApiController]
	[Route("Items")]
	public class ItemsController : ReheeCmfController
	{
		public ItemsController(IServiceProvider sp, IItemService itemService) : base(sp)
		{
			this.itemService = itemService;
		}

		private IItemService itemService { get; }

		[HttpGet("Pick/{actorId}")]
		[CmfAuthorize(AuthOnly = true)]
		public async Task<IEnumerable<ItemEntity>?> PickableItems(Guid? actorId)
		{
			if (actorId == null)
			{
				return Enumerable.Empty<ItemEntity>();
			}
			return await itemService.PickableItems(actorId.Value);
		}
		[HttpPost("Pick/{actorId}")]
		[CmfAuthorize(AuthOnly = true)]
		public async Task<bool> PickItems(Guid? actorId, Guid[]? items)
		{
			if (actorId == null || items?.Any() != true)
			{
				return false;
			}
			var result = await itemService.PickItem(actorId, items);
			return result.Success;
		}
		[HttpGet("Inventory/{actorId}")]
		[CmfAuthorize(AuthOnly = true)]
		public async Task<IEnumerable<ItemEntity>?> Inventory(Guid? actorId)
		{
			var result = await itemService.ActorInventory(actorId);
			return await itemService.ActorInventory(actorId);
		}
		[HttpPost("Equip/{actorId}/{itemId}/{slot?}")]
		[CmfAuthorize(AuthOnly = true)]
		public async Task<bool> Inventory(Guid? actorId, Guid? itemId, EnumEquipSlot? slot)
		{
			var result = await itemService.EquipItem(actorId, itemId, slot);
			return result.Success;
		}
		[HttpPost("UnEquip/{actorId}")]
		[CmfAuthorize(AuthOnly = true)]
		public async Task<bool> UnEquip(Guid? actorId, EnumEquipSlot[] slot)
		{
			var result = await itemService.UnEquipItem(actorId, slot);
			return result.Success;
		}

		[HttpDelete("Drop/Pickup/{actorId}")]
		[CmfAuthorize(AuthOnly = true)]
		public async Task<bool> DropPickup(Guid? actorId, Guid[] itemId)
		{
			var result = await itemService.DropPickupItem(actorId, itemId);
			return result.Success;
		}
		[HttpDelete("Drop/Backpack/{actorId}")]
		[CmfAuthorize(AuthOnly = true)]
		public async Task<bool> DropBackpackItem(Guid? actorId, Guid[] itemId)
		{
			var result = await itemService.DropBackpackItem(actorId, itemId);
			return result.Success;
		}
		[HttpPost("Sell/Backpack/{actorId}")]
		[CmfAuthorize(AuthOnly = true)]
		public async Task<bool> SellBackpackItem(Guid? actorId, Guid[] itemId)
		{
			var result = await itemService.SellBackpackItem(actorId, itemId);
			return result.Success;
		}
	}
}
