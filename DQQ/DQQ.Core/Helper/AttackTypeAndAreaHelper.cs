using DQQ.Combats;
using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Helper
{
	public static class AttackTypeAndAreaHelper
	{
		public static void AreaLevelChange(this IWIthAttackTypeAndArea input, int levelChange = 1)
		{
			var intValue = (int)input.AreaLevel + levelChange;
			intValue = Math.Max((int)EnumAreaLevel.Self, intValue);
			intValue = Math.Min((int)EnumAreaLevel.TargetWithRadiusMax, intValue);
			input.AreaLevel = (EnumAreaLevel)intValue;
		}

		public static void SetAttackAndAreaType(this IWIthAttackTypeAndArea to,
			IWIthAttackTypeAndArea? from)
		{
			to.AttackTypes = from?.AttackTypes ?? EnumAttackType.NotSpecified;
			to.AreaLevel = from?.AreaLevel ?? EnumAreaLevel.Single;
			to.ExtraAttackNumber = from?.ExtraAttackNumber ?? 0;
		}

		public static void SetAttackAndAreaType(this IWIthAttackTypeAndArea to,
			IWIthAttackTypeAndArea? from,
			IEnumerable<ISetAttackTypeAndArea?>? supports)
		{
			to.SetAttackAndAreaType(from);
			if (supports?.Any() == true)
			{
				foreach (var s in supports)
				{
					s?.SetAttackTypeAndArea(to);
				}
			}
		}
	}
}
