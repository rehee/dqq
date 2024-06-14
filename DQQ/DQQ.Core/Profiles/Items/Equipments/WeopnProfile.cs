using DQQ.Components.Items.Equips;
using DQQ.Enums;
using DQQ.Helper;
using System.Numerics;

namespace DQQ.Profiles.Items.Equipments
{
	public abstract class WeopnProfile : EquipProfile
	{
		public abstract decimal AttackPerSecond { get; }
		public abstract decimal DamageMultiple { get; }

		public override EquipComponent GenerateEquipComponent(Random r, int? itemLevel, EnumRarity rarity = EnumRarity.Normal)
		{
			if (itemLevel <= 0)
			{
				itemLevel = 1;
			}
			var equip = base.GenerateEquipComponent(r, itemLevel, rarity);
			equip.Property!.AttackPerSecond = AttackPerSecond;
			var baseDamage = itemLevel.DefaultValue(1).WeponDamageIncrease(r, DamageMultiple);
			var attackPerHit = (int)Math.Round(baseDamage / AttackPerSecond, 0);
			if (attackPerHit <= 0)
			{
				attackPerHit = 1;
			}
			switch (EquipType)
			{
				case EnumEquipType.OneHandWeapon:
					equip.Property!.MainHand = attackPerHit;
					equip.Property!.OffHand = attackPerHit;
					break;
				case EnumEquipType.TwoHandWeapon:
				case EnumEquipType.MainHandWeapon:
					equip.Property!.MainHand = attackPerHit;
					break;
			}

			return equip;
		}
	}
}
