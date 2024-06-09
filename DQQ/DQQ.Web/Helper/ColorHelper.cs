using BootstrapBlazor.Components;

namespace DQQ.Helper
{
	public static class ColorHelper
	{
		public static Color GetColor(this IWithRarity? input)
		{
			if (input == null)
			{
				return Color.None;
			}
			switch (input?.Rarity)
			{

				case Enums.EnumRarity.Normal:
					return Color.Dark;
				case Enums.EnumRarity.Magic:
					return Color.Primary;
				case Enums.EnumRarity.Rare:
					return Color.Warning;
			}

			return Color.None;
		}

		public static string UnAvliable(this Color c)
		{
			return c == Color.Danger ? "[无法生效]" : "";
		}
	}
}
