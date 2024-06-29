using DQQ.Components.Stages.Actors.Characters;
using DQQ.Enums;
using DQQ.Pools;
using DQQ.Profiles.Items.Equipments;
using DQQ.Profiles.Items;
using DQQ.Services.ActorServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReheeCmf.Authenticates;
using ReheeCmf.Modules.Controllers;
using static Grpc.Core.Metadata;
using System;
using DQQ.Api.Services.Itemservices;
using DQQ.Services.ItemServices;

namespace DQQ.Api.Apis
{
  [ApiController]
  [Route("Character")]
  public class CharacterController : ReheeCmfController
  {
    private readonly ICharacterService cService;
		private readonly ITemporaryService tservice;
		private readonly IItemService itemService;

		public CharacterController(IServiceProvider sp, ICharacterService cService, ITemporaryService tservice, IItemService itemService) : base(sp)
    {
      this.cService = cService;
			this.tservice = tservice;
			this.itemService = itemService;
		}

    [Route("")]
    [HttpGet]
    [CmfAuthorize(AuthOnly = true)]
    public async Task<IEnumerable<Character>> GetAllCharacter()
    {
      return await cService.GetAllCharacters();
    }
    [Route("{Id}")]
    [HttpGet]
    [CmfAuthorize(AuthOnly = true)]
    public async Task<Character> GetCharacter(Guid? Id)
    {
      if (Id == null)
      {
        return null;
      }
      return await cService.GetCharacter(Id.Value);
    }
    [Route("")]
    [HttpPost]
    [CmfAuthorize(AuthOnly = true)]
    public async Task<Guid?> CreateCharacter(Character c)
    {
      await Task.CompletedTask;
      if (String.IsNullOrEmpty(c?.DisplayName))
			{
				return null;
			}
			c.OwnerId = currentUser?.UserId;
      var result = await cService.CreateCharacter(c);

      if (result.Success)
      {
				var sword = (DQQPool.TryGet<ItemProfile, EnumItem?>(EnumItem.CopperSword) as EquipProfile)!.GenerateComponent(new Random(), 1, 1);
				await tservice.AddAndIntoTemporary(result.Content, sword);
        await itemService.PickItem(result.Content, sword.DisplayId!.Value);
				await itemService.EquipItem(result.Content, sword.DisplayId!.Value, EnumEquipSlot.MainHand);
			}
			

			return result.Content;
    }
  }
}
