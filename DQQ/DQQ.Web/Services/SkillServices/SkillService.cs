using DQQ.Commons.DTOs;
using DQQ.Components.Skills;
using DQQ.Entities;
using DQQ.Helper;
using DQQ.Pools;
using DQQ.Services;
using DQQ.Services.ActorServices;
using DQQ.Services.SkillServices;
using DQQ.Web.Datas;
using DQQ.Web.Services.Requests;
using ReheeCmf.Helpers;
using ReheeCmf.Requests;
using ReheeCmf.Responses;

namespace DQQ.Web.Services.SkillServices
{
	public class SkillService : ClientServiceBase, ISkillService
	{
		
		public SkillService(RequestClient<DQQGetHttpClient> client, IIndexRepostory repostory, IGameStatusService statusService) : base(client, repostory, statusService)
		{
			
		}

		public async Task<IEnumerable<SkillDTO>> GetAllSkills()
		{
			await Task.CompletedTask;
			return DQQPool.SkillPool.Select(b => b.Value).PlayerAvaliableSkill().Select(b => new SkillDTO { SkillNumber = b.SkillNumber }).ToArray();
		}

		public async Task<ContentResponse<bool>> PickSkill(PickSkillDTO? dto)
		{
			if(await IsOnleService())
			{
				var response = await client.Request<bool>(HttpMethod.Post, "Skills", dto?.ToJson());
				return response;
			}
			var result = new ContentResponse<bool>();
			var status = await StatusService.GetOrCreateGameStatus();
			return await Repostory.Update<OfflineCharacter>(dto?.ActorId, character => Task.Run(async () =>
			{
				await Task.CompletedTask;
				var entity = dto?.ToSkillEntity(character?.SelectedCharacter);
				if (character.SelectedCharacter.Skills == null)
				{
					character.SelectedCharacter.Skills = new List<SkillComponent>();
				}
				var component = entity?.ToSkillComponent(
					character?.SelectedCharacter,
					character?.SelectedCharacter?.WithTwoHandWeapon,
					character?.SelectedCharacter?.WithWeapon1,
					character?.SelectedCharacter?.WithWeapon2);
				if (component != null)
				{
					var list = character?.SelectedCharacter?.Skills?
						.Where(b => b.Slot != component.Slot)
						.Where(b=>b.SkillNumber != component.SkillNumber)?.ToList();
					list?.Add(component);
					character.SelectedCharacter.Skills = list;
					character.SelectedCharacter.SkillMap = character.SelectedCharacter.Skills.ToSkillDictionary(character.SelectedCharacter);
				}
			}));
			

		}

		public Task<ContentResponse<bool>> PickSkills(Guid? actorId, params PickSkillDTO?[] dtos)
		{
			throw new NotImplementedException();
		}
	}
}
