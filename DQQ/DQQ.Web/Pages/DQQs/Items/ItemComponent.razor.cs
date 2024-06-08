using BootstrapBlazor.Components;
using DQQ.Components.Affixes;
using DQQ.Components.Items;
using DQQ.Entities;
using DQQ.Enums;
using Microsoft.AspNetCore.Components;
using ReheeCmf.Commons.Jsons.Options;
using System.Text.Json;

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
		public EnumEquipSlot? Slot { get; set; }

		public string SlotName => Slot.HasValue ? $"[{Slot.GetEnumString()}]" : "";


		public (string?, int?)[]? Footer { get; set; }

		public string[] ItemTags { get; set; } = [];

		protected override Task OnParametersSetAsync()
		{
			if (Item == null)
			{
				ItemTags = [];
				Footer = null;
			}
			else
			{
				string?[] tags = [
					(Item?.ItemType).GetEnumString(),
					(Item?.EquipType).GetEnumString(),
					(Item?.ItemNumber).GetEnumString()
					];
				ItemTags = tags.Distinct().Where(b => !String.IsNullOrEmpty(b)).Select(b => b!).ToArray();

				var componentFooter = new List<(string?, int?)>();
				try
				{
					var affixes = JsonSerializer.Deserialize<AffixeComponent[]?>(Item?.AffixesJson ?? "[]", JsonOption.DefaultOption);

					if (affixes?.Any() == true)
					{
						var ag = affixes.GroupBy(b => b.AffixeProfile?.IsPrefix == true).OrderByDescending(b => b.Key);

						foreach (var a in ag)
						{
							foreach (var aa in a)
							{
								componentFooter.Add((aa.AffixeProfile?.Name, aa.AffixeProfile?.TierLevel));
							}
						}
						componentFooter = null;
						Footer = componentFooter?.ToArray();
					}
				}
				catch
				{

				}
			}

			return Task.CompletedTask;
		}

	}
}