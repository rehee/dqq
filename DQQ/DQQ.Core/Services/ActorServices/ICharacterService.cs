using DQQ.Components.Stages.Actors.Characters;
using ReheeCmf.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Services.ActorServices
{
	public interface ICharacterService
	{
		Task<ContentResponse<Guid?>> CreateCharacter(Character? character);
		Task<IEnumerable<Character>> GetAllCharacters();
		Task<Character?> GetCharacter(Guid? charId);
		Task<ContentResponse<Guid?>> DeleteCharacter(Guid? charId);

		bool SelectedCharacter(Guid? charId);
		Guid? GetSelectedCharacter();

		Task<ContentResponse<bool>> GainExperience(Guid? charId, string? exp);
	}
}
