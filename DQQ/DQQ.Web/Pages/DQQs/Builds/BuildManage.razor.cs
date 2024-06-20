using DQQ.Commons.DTOs;
using DQQ.Components.Stages.Actors.Characters;
using DQQ.Services.BDServices;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace DQQ.Web.Pages.DQQs.Builds
{
	public class BuildManagePage : DQQPageBase
	{
		[Inject]
		[NotNull]
		public IBDService? bdService { get; set; }

		public BuildSummaryDTO[]? BuildDTOs { get; set; }

		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();

		}

		public async Task CreateBD()
		{
			await bdService.CreateNewBuild(new BuildDTO
			{
				ActorId = ActorId,
				BuildName = "new bd"
			});
			await Refresh();
		}
		public async Task Apply(Guid? id)
		{
			await bdService.ApplyBuild(new BuildDTO
			{
				ActorId = ActorId,
				BuildId = id,
			});
			await Refresh();
			ParentRefreshEvent?.InvokeEvent(this, new EventArgs { });
		}
		public async Task Delete(Guid? id)
		{
			await bdService.DeleteBuild(new BuildDTO
			{
				ActorId = ActorId,
				BuildId = id,
			});
			await Refresh();

		}
		public async Task Update(Guid? id)
		{
			await bdService.UpdateBuild(new BuildDTO
			{
				ActorId = ActorId,
				BuildId = id,
			});
			await Refresh();

		}



		protected override async Task OnParametersSetAsync()
		{
			await base.OnParametersSetAsync();
			await Refresh();
		}

		public async Task Refresh()
		{
			if (ActorId == null)
			{
				BuildDTOs = [];
			}
			else
			{
				var result = await bdService.GetAllBuild(ActorId);
				if (result?.Any() == true)
				{
					BuildDTOs = result.ToArray();
				}
				else
				{
					BuildDTOs = [];
				}
			}
		}
	}
}