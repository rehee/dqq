using BootstrapBlazor.Components;
using DQQ.Entities;
using DQQ.Enums;
using DQQ.Services.ItemServices;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace DQQ.Web.Pages.DQQs.Inventories
{
	public abstract class InventoryCommon : DQQPageBase
	{
		public const int ItemPerPage = 50;
		[Parameter]
		public EnumEquipSlot? SelectedSlot { get; set; }

		[Inject]
		[NotNull]
		public IItemService? itemService { get; set; }

		public IEnumerable<ItemEntity>? Items { get; set; }

		public IEnumerable<ItemEntity>? PagedItems => Items?.Skip(PageCount * (PageSelected - 1)).Take(ItemPerPage);

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
		public int PageSelected { get; set; } = 1;
		public async Task PageSelect(int index)
		{
			await Task.CompletedTask;
			PageSelected = index;
			StateHasChanged();
		}
		public async Task ListRefresh()
		{
			var currentItem = Items?.ToArray();
			Items = (SelectedSlot.HasValue ? Source?.Where(b => b.GetAvaliableSlots()?.Any(b => b == SelectedSlot) == true) :
				Source)?.
				Where(b =>
				{
					if (IsShowCurrency)
					{
						return b.Profile?.IsStack == true;
					}
					var reality = RaritySelect.Where(r => r.Rarity == b.Rarity).FirstOrDefault();
					if (reality == null)
					{
						return true;
					}
					return reality.Selected;
				})?.
				OrderByDescending(b => b.ItemLevel).ToArray();
			PageSelected = 1;
			PageCount = (int)Math.Ceiling((Items?.Count() ?? 0) / (double)ItemPerPage);

			var listKeepSame = false;
			if(currentItem!=null && Items != null)
			{
				var currentItemIds = currentItem.Select(b => b.Id);
				var itemsId = Items.Select(b => b.Id);
				var intersect = currentItemIds.Intersect(itemsId).Count();
				listKeepSame = itemsId.Count() == intersect;
			}
			if (!listKeepSame)
			{
				CleanMultiSelect();
			}
			
			await Task.CompletedTask;
		}
		public bool IsShowCurrency { get; set; }
		
		[Parameter]
		public bool IsMultiSelect { get; set; }

		[Parameter]
		public EventCallback<bool> IsMultiSelectCallback { get; set; }

		public async Task ShowCurrency()
		{
			await Task.CompletedTask;
			IsShowCurrency = !IsShowCurrency;
			await ListRefresh();
		}
		protected void CleanMultiSelect()
		{
			if (Source != null)
			{
				foreach (var item in Source)
				{
					item.IsSelected = false;
				}
			}
		}
		public async Task MultiSelectChange()
		{
			await Task.CompletedTask;
			IsMultiSelect = !IsMultiSelect;
			if (IsMultiSelectCallback.HasDelegate)
			{
				IsMultiSelectCallback.InvokeAsync(IsMultiSelect);
			}
			CleanMultiSelect();
			
		}
		public async Task SelectAll()
		{
			await Task.CompletedTask;
			if (PagedItems?.Any()!=true)
			{
				return;
			}
			var selectedAll = PagedItems?.All(b => b.IsSelected) == true;
			foreach (var p in PagedItems!)
			{
				p.IsSelected = !selectedAll;
			}
			
		}
		public async Task CheckboxStateChange(CheckboxState state, bool isChecked)
		{
			await ListRefresh();

		}
		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();

		}

		[Parameter]
		public EventCallback<ItemEntity?> OnItemClicked { get; set; }

		[Parameter]
		public ItemEntity? ItemSelected { get; set; }


		[Parameter]
		public EventCallback<bool> OpenOrClose { get; set; }


	}

	public class ItemRaritySelect : IWithRarity
	{
		public static ItemRaritySelect New(EnumRarity rarity, bool selected = true)
		{
			return new ItemRaritySelect
			{
				Label = rarity.GetEnumString(),
				Rarity = rarity,
				Selected = selected
			};
		}
		public string? Label { get; set; }
		public bool Selected { get; set; } = true;
		public EnumRarity Rarity { get; set; }
	}
}
