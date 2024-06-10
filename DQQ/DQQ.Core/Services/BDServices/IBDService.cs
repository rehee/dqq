using DQQ.Commons.DTOs;
using DQQ.Components.Stages.Actors.Characters;
using DQQ.Entities;
using ReheeCmf.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Services.BDServices
{
	public interface IBDService
	{
		Task<ContentResponse<bool>> CreateNewBuild(BuildDTO dto);
		Task<ContentResponse<bool>> UpdateBuild(BuildDTO dto);
		Task<ContentResponse<bool>> DeleteBuild(BuildDTO dto);
		Task<ContentResponse<bool>> ApplyBuild(BuildDTO dto);
		Task<IEnumerable<BuildSummaryDTO>?> GetAllBuild(Guid? actorId);
		
	}
}
