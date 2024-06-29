using DQQ.Components.Stages.Actors.Characters;
using DQQ.Consts;

namespace DQQ.Helper
{
	public static class InventoryHelp
	{
		public static int GetInventoryPickupLimit(this Character? character)
		{
			return DQQGeneral.InventoryPickupLimit;
		}
		public static int GetInventoryBackpackLimit(this Character? character)
		{
			return DQQGeneral.InventoryBackpackLimit;
		}
	}
}
