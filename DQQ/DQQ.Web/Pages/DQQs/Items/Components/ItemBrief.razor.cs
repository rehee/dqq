using DQQ.Entities;
using Microsoft.AspNetCore.Components;
using ReheeCmf.Helpers;

namespace DQQ.Web.Pages.DQQs.Items.Components
{
	public  class ItemBriefPage: DQQPageBase
	{
		[Parameter]
		public ItemEntity? Item { get; set; }

		[Parameter]
		public ItemEntity? ItemSelected { get; set; }
		
		[Parameter]
		public EventCallback<ItemEntity?> OnItemClicked { get; set; }

		[Parameter]
		public EventCallback<bool> OpenOrClose { get; set; }
		[Parameter]
		public bool IsMultiSelect { get; set; }

		public bool SelfSelected => Item != null && Item.Id == ItemSelected?.Id;
		public string ContentClass => $"{(SelfSelected ? "border-success" : "border-dark")} border  square-content";

		public bool IsMultiSelectedItem => IsMultiSelect && Item?.IsSelected == true;

		public Task OnItemSelected(ItemEntity? item)
		{
			

			if (OnItemClicked.HasDelegate)
			{
				OnItemClicked.InvokeAsync(item);
			}
			if (OpenOrClose.HasDelegate)
			{
				OpenOrClose.InvokeAsync(true);
			}
			return Task.CompletedTask;
		}
	}
}