using BootstrapBlazor.Components;
using DQQ.Enums;
using System.ComponentModel;
using System.Reflection.PortableExecutable;

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

		public static Color GetColor(this EnumTargetLevel input)
		{
			switch (input)
			{
				case EnumTargetLevel.NotSpecified:
				case EnumTargetLevel.Normal:
					return Color.Dark;
				case EnumTargetLevel.Magic:
					return Color.Primary;
				case EnumTargetLevel.Elite:
					return Color.Info;
				case EnumTargetLevel.Champion:
					return Color.Warning;
				case EnumTargetLevel.Guardian:
					return Color.Danger;
			}


			return Color.Dark;

		}


		public static string UnAvliable(this Color c)
		{
			return c == Color.Danger ? "[无法生效]" : "";
		}
	}
}
