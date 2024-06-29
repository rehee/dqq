using BootstrapBlazor.Components;
using DQQ.Entities;
using DQQ.Enums;
using Microsoft.AspNetCore.Components;

namespace DQQ.Web.Pages.DQQs.Items
{
	public class ItemComponentPage: DQQPageBase
	{

		public Color ThisColor =>
			Item?.Rarity == EnumRarity.Rare ? Color.Warning :
			Item?.Rarity == EnumRarity.Magic ? Color.Primary :
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
					if (Item?.Affixes.Any() == true)
					{
						var ag = Item?.Affixes.GroupBy(b => b.AffixeProfile?.IsPrefix == true).OrderByDescending(b => b.Key);
						if(ag?.Any() == true)
						{
							foreach (var aa in ag)
							{
								foreach (var aaa in aa)
								{
									componentFooter.Add((aaa?.AffixeProfile?.Name, aaa?.AffixeProfile?.TierLevel));
								}
							}
						}
						
						Footer = componentFooter?.ToArray();
						componentFooter = null;
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