using BootstrapBlazor.Components;
using DQQ.Entities;
using Microsoft.AspNetCore.Components;

namespace DQQ.Web.Pages.DQQs.Items
{
	public partial class ItemComponent
	{

		public Color ThisColor =>
			Item?.Rarity == Enums.EnumRarity.Rare ? Color.Warning :
			Item?.Rarity == Enums.EnumRarity.Magic ? Color.Primary :
			Color.None;

		[Parameter]
		public ItemEntity? Item { get; set; }

		[Parameter]

		public (string?, int?)[]? Footer { get; set; }

		public string[] ItemTags { get; set; } = [];

		protected override Task OnParametersSetAsync()
		{
			if (Item == null)
			{
				ItemTags = [];
			}
			else
			{
				string?[] tags = [
					(Item?.ItemType).GetStragyEnumString(),
					(Item?.EquipType).GetStragyEnumString(),
					(Item?.ItemNumber).GetStragyEnumString()
					];
				ItemTags = tags.Distinct().Where(b => !String.IsNullOrEmpty(b)).Select(b => b!).ToArray();
			}

			return Task.CompletedTask;
		}

	}
}