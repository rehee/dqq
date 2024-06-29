using DQQ.Components.Items;
using DQQ.Enums;
using DQQ.Profiles.Items;
using DQQ.Profiles.Items.Currencies;
using DQQ.Profiles.Items.Equipments;

namespace DQQ.Pools
{
  public static partial class DQQPool
  {
    public static Dictionary<EnumItem, ItemProfile> ItemPool { get; set; } = new Dictionary<EnumItem, ItemProfile>();

    public static ItemComponent? GenerateItemComponent(this EnumItem item, Random r, int? itemLevel, int? quantity, EnumRarity rarity = EnumRarity.Normal) 
    {
      var profile = DQQPool.TryGet<ItemProfile, EnumItem?>(item);
      if (profile == null)
      {
        return null;
      }
      if(profile is EquipProfile equipProfile)
			{
				return equipProfile.GenerateComponent(r, itemLevel??1, 1, rarity);
			}
      if(profile is AbCurrency currencyProfile)
      {
        return currencyProfile.GenerateComponent(r,1,quantity??1, rarity);
			}
      return profile.GenerateComponent(r, itemLevel ?? 1, quantity ?? 1, rarity);

		}
  }
}
