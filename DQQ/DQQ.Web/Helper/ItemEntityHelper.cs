using BootstrapBlazor.Components;
using DQQ.Components.Affixes;
using DQQ.Entities;
using ReheeCmf.Commons.Jsons.Options;
using System.Text.Json;

namespace DQQ.Helper
{
	public static class ItemEntityHelper
	{
		public static async Task SetComponent(this ItemEntity? item, IComponentHtmlRenderer? renderer)
		{
			if (item == null || renderer == null)
			{
				return;
			}
			var componentFooter = new List<(string?, int?)>();
			try
			{
				var affixes = JsonSerializer.Deserialize<AffixeComponent[]?>(item.AffixesJson, JsonOption.DefaultOption);

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
				}
			}
			catch
			{

			}

			item.SetComponentString(await renderer!.RenderAsync<Web.Pages.DQQs.Items.ItemComponent>(
				new Dictionary<string, object?>()
				{
					["Item"] = item,
					["Footer"] = componentFooter.ToArray(),
				}));
		}
	}
}
