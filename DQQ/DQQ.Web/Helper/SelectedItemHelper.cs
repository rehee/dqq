using BootstrapBlazor.Components;
using DQQ.Enums;
using DQQ.Profiles.Maps;
using Newtonsoft.Json.Linq;

namespace DQQ.Helper
{
	public static class SelectedItemHelper
	{
		public static IEnumerable<SelectedItem>? GetSelectedItem<T>(bool withna = true)
		{
			var checkType = typeof(T);
			if (checkType == typeof(bool))
			{
				return GetSelectedItemBool();
			}
			if (checkType.IsEnum)
			{
				var enumValues = (T[])Enum.GetValues(checkType);
				var enumResult = new List<SelectedItem>();
				if (withna)
				{
					enumResult.Add(new SelectedItem($"", $"N/A"));
				}
				foreach (var value in enumValues)
				{
					var valueString = value.GetEnumString();
					if (string.IsNullOrEmpty(valueString))
					{
						continue;
					}
					enumResult.Add(new SelectedItem($"{value}", valueString!));
				}
				return enumResult;
			}
			return Enumerable.Empty<SelectedItem>();
		}
		public static IEnumerable<SelectedItem>? GetSelectedItemBool()
		{
			return new SelectedItem[]
			{
				new SelectedItem("true","是"),
				new SelectedItem("false","否"),
			};
		}
		public static IEnumerable<SelectedItem>? GetSelectedItemEnum()
		{


			return new SelectedItem[]
			{
				new SelectedItem("true","是"),
				new SelectedItem("false","否"),
			};
		}
		public static IEnumerable<SelectedItem>? GetSelectedItemEnum(this IEnumerable<MapProfile> maps)
		{

			return [new SelectedItem("", "n/a"), .. maps.Select(b => new SelectedItem($"{b.ProfileNumber}", b.Name ?? ""))];


		}
	}
}
