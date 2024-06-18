using DQQ.Components.Stages.Actors.Characters;
using DQQ.Enums;
using DQQ.Pools;
using DQQ.Profiles.ZProgress;

namespace DQQ.Helper
{
	public static class ProgressHelper
	{
		public static bool IsUnlocked(this EnumProgress progress, Character? player)
		{
			if (player == null) return false;
			var p = DQQPool.TryGet<ProgressProfile, EnumProgress>(progress);
			return p?.AvaliableCheck(player) == true;
		}
	}
}
