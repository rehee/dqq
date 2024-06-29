using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Items.Currencies
{
	public abstract class AbCurrency: ItemProfile
	{
		public override bool IsStack => true;
		public override int DropQuantity => 1;
		public override EnumItemType? ItemType => EnumItemType.Currency;

		public static AbCurrency? New(EnumRarity rarity)
		{
			switch (rarity)
			{
				case EnumRarity.Normal:
					return new GeneralMaterial();
				case EnumRarity.Magic:
					return new MagicDust();
				case EnumRarity.Rare:
					return new Crystal();
			}
			return null;
		}
	}
}
