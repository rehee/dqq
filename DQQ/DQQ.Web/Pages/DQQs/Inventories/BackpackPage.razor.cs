using BootstrapBlazor.Components;
using DQQ.Entities;
using DQQ.Enums;
using DQQ.Services.ItemServices;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Emit;

namespace DQQ.Web.Pages.DQQs.Inventories
{
	public class BackpackPagePage: DQQPageBase
	{
		public const int ItemPerPage = 50;
		[Parameter]
		public EnumEquipSlot? SelectedSlot { get; set; }

		[Parameter]
		public EventCallback<ItemEntity?> OnItemClicked {  get; set; }

		[Parameter]
		public ItemEntity? ItemSelected { get; set; } 

		[Inject]
		[NotNull]
		public IItemService? itemService { get; set; }
		public IEnumerable<ItemEntity>? Items { get; set; }

		public IEnumerable<ItemEntity>? PagedItems => Items?.Skip(PageCount* (PageSelected-1)).Take(ItemPerPage);

		public ItemRaritySelect[] RaritySelect { get; set; } = [
			ItemRaritySelect.New(EnumRarity.Normal),
			ItemRaritySelect.New(EnumRarity.Magic),
			ItemRaritySelect.New(EnumRarity.Rare),
			];

		[Parameter]
		public ItemEntity[]? Source { get; set; }

		protected override async Task OnParametersSetAsync()
		{
			await base.OnParametersSetAsync();
			await ListRefresh();
			StateHasChanged();
		}
		public int PageCount { get; set; }
		public int PageSelected {  get; set; } = 1;
		public async Task PageSelect(int index)
		{
			await Task.CompletedTask;
			PageSelected = index;
			StateHasChanged();
		}
		public async Task ListRefresh()
		{
			Items = (SelectedSlot.HasValue ? Source?.Where(b => b.GetAvaliableSlots()?.Any(b => b == SelectedSlot) == true) :
				Source)?.
				Where(b =>
				{
					if (IsShowCurrency)
					{
						return b.Profile?.IsStack == true;
					}
					var reality = RaritySelect.Where(r=>r.Rarity==b.Rarity).FirstOrDefault();
					if(reality == null)
					{
						return true;
					}
					return reality.Selected;
				})?.
				OrderByDescending(b => b.ItemLevel).ToArray();
			PageSelected = 1;
			PageCount = (int)Math.Ceiling((Items?.Count() ?? 0) / (double)ItemPerPage);
			await Task.CompletedTask;
		}
		public bool IsShowCurrency { get; set; }
		public async Task ShowCurrency()
		{
			await Task.CompletedTask;
			IsShowCurrency = !IsShowCurrency;
			await ListRefresh();
		}
		public async Task CheckboxStateChange(CheckboxState state, bool isChecked)
		{
			await ListRefresh();

		}
		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();
			
		}

		
	}


	public class ItemRaritySelect: IWithRarity
	{
		public static ItemRaritySelect New(EnumRarity rarity,bool selected = true)
		{
			return new ItemRaritySelect
			{
				Label = rarity.GetEnumString(),
				Rarity = rarity,
				Selected= selected
			};
		}
		public string? Label { get; set; }
		public bool Selected { get; set; } = true;
		public EnumRarity Rarity { get; set; }
	}
}