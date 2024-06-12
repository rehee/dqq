using DQQ.Combats;
using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Helper
{
	public static class AreaLevelHelper
	{
		public static void AreaLevelChange(this IWithAreaLevel input, int levelChange = 1)
		{
			if (input.AreaLevel == null)
			{
				input.AreaLevel = EnumAreaLevel.Single;
				return;
			}
			var intValue = (int)input.AreaLevel + levelChange;

			intValue = Math.Min((int)EnumAreaLevel.Self, intValue);
			intValue = Math.Max((int)EnumAreaLevel.TargetWithRadiusMax, intValue);
			input.AreaLevel = (EnumAreaLevel)intValue;
		}
	}
}
